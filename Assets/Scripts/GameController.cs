using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CardPool cardPool;
    [SerializeField]
    private HandController[] hands = new HandController[2];
    private CardDeck deck;

    [SerializeField]
    private MidPileController midPileController;

    private int currentPlayer = 0;
    private BotLogic botLogic = new BotLogic();

    [SerializeField]
    private List<Card> playedCards = new List<Card>();

    private bool isFirstRound = true;

    void Start()
    {
        isFirstRound = true;
        cardPool.createPool();
        cardPool.gameObject.SetActive(true);
        deck = new CardDeck();
        foreach (HandController hand in hands)
        {
            hand.ResetScore();
        }
        ScoreUiManager.Init(hands);
        ScoreUiManager.UpdateScores();
        foreach (HandController hand in hands)
        {
            hand.GameController = this;
        }
        StartCoroutine(StartRound());
    }

    CardController GetNewCard()
    {
        CardController cardController = cardPool.getCard();
        cardController.gameObject.SetActive(true);
        Card card = deck.DrawCard();
        cardController.SetCardValue(card.value, card.suit);
        return cardController;
    }

    float DealHand(HandController hand)
    {

        for (int i = 0; i < 4; i++)
        {
            bool isFaceUp = hand.IsPlayer;
            CardController cardController = GetNewCard();
            hand.AddCard(cardController);
            cardController.CardView.SetCardFace(isFaceUp, 0.15f * i);
        }
        float duration = hand.PositionCards(0.15f);

        if (deck.RemainingCardsLength() < 1)
        {
            cardPool.gameObject.SetActive(false);
        }

        return duration;
    }

    float DealMidPile()
    {
        float maxDuration = 0;
        for (int i = 0; i < 4; i++)
        {
            bool isFaceUp = i == 3;
            CardController cardController = GetNewCard();
            float duration = midPileController.AddCard(cardController, i * 0.1f);
            cardController.CardView.SetCardFace(isFaceUp, 0.1f * i);
            if (duration > maxDuration)
            {
                maxDuration = duration;
            }

            if (i == 3)
            {
                playedCards.Add(new Card(cardController.CardModel.Value, cardController.CardModel.SuitName));
            }
        }
        return maxDuration;
    }

    IEnumerator StartRound()
    {
        bool allHandsEmpty = true;
        foreach (HandController hand in hands)
        {
            if (hand.Cards.Count > 0)
            {
                allHandsEmpty = false;
                break;
            }
        }

        if (allHandsEmpty)
        {
            if (deck.RemainingCardsLength() > 0)
            {
                for (int i = 0; i < hands.Length; i++)
                {
                    float duration = DealHand(hands[i]);
                    yield return new WaitForSeconds(duration);
                }
            }
            else
            {
                Debug.Log("Game Over");
                Start();
                yield break;
            }
        }

        if (isFirstRound)
        {
            float duration = DealMidPile();
            isFirstRound = false;
            yield return new WaitForSeconds(duration);
        }

        if (hands[currentPlayer].IsPlayer)
        {
            hands[currentPlayer].EnableInputForAllCards();
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
            CardController playedCard = botLogic.PlayCard(hands[currentPlayer].Cards, playedCards);
            hands[currentPlayer].PlayCard(playedCard);
            playedCard.CardView.SetCardFace(true, 0.1f);
            StartCoroutine(CardPlayed(playedCard));
        }
    }

    private IEnumerator EndRound()
    {
        hands[currentPlayer].DisableInputForAllCards();
        yield return new WaitForSeconds(CheckedPlayedCard());
        ScoreUiManager.UpdateScores();
        EmptyStash(hands[currentPlayer]);
        currentPlayer = (currentPlayer + 1) % hands.Length;
        StartCoroutine(StartRound());
    }

    private float CheckedPlayedCard()
    {
        if (midPileController.Cards.Count < 2)
        {
            return 0;
        }

        CardController lastPlayedCard = midPileController.Cards[midPileController.Cards.Count - 1];
        if (lastPlayedCard.CardModel.Value == 11)
        {
            midPileController.UpdateScore();
            hands[currentPlayer].AddScore(midPileController.CurrentScore);
            return CollectCards(hands[currentPlayer]);
        }

        CardController midCard = midPileController.Cards[midPileController.Cards.Count - 2];
        if (lastPlayedCard.CardModel.Value == midCard.CardModel.Value)
        {
            midPileController.UpdateScore();
            hands[currentPlayer].AddScore(midPileController.CurrentScore);
            return CollectCards(hands[currentPlayer]);
        }

        return 0;
    }

    public IEnumerator CardPlayed(CardController cardController)
    {
        yield return new WaitForSeconds(midPileController.AddCard(cardController));
        playedCards.Add(new Card(cardController.CardModel.Value, cardController.CardModel.SuitName));
        StartCoroutine(EndRound());
    }

    float CollectCards(HandController hand)
    {
        return hand.CollectCardsToStash(midPileController.GetCardsAndEmptyPile());
    }

    void EmptyStash(HandController hand)
    {
        foreach (CardController stashCard in hand.StashCards)
        {
            cardPool.returnCard(stashCard);
        }
        hand.emptyStash();
    }


}
