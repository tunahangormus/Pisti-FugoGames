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

    [SerializeField]
    private EndCardController endCardController;

    private bool isFirstRound = true;

    void Start()
    {
        isFirstRound = true;
        cardPool.createPool();
        deck = new CardDeck();
        ScoreUiManager.Init(hands);
        ScoreUiManager.UpdateScores();
        foreach (HandController hand in hands)
        {
            hand.GameController = this;
        }
        StartCoroutine(StartRound());
    }

    public void Restart()
    {
        cardPool.gameObject.SetActive(true);
        foreach (HandController hand in hands)
        {
            hand.ResetScore();
        }
        List<CardController> midPileCards = midPileController.GetCardsAndEmptyPile();
        foreach (CardController card in midPileCards)
        {
            cardPool.ReturnCard(card);
        }
        endCardController.HideEndCard();
        Start();
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
        if (isFirstRound)
        {
            yield return new WaitForSeconds(0.5f);
        }
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
                EndGame();
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
            CardController playedCard = botLogic.PlayCard(hands[currentPlayer].Cards, playedCards, midPileController.Cards);
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
            cardPool.ReturnCard(stashCard);
        }
        hand.emptyStash();
    }

    void EndGame()
    {

        int maxStashCount = 0;
        int winnerHandIndex = 0;

        for (int i = 0; i < hands.Length; i++)
        {
            if (hands[i].StashCount > maxStashCount)
            {
                maxStashCount = hands[i].StashCount;
                winnerHandIndex = i;
            }
        }

        hands[winnerHandIndex].AddScore(3);
        ScoreUiManager.UpdateScores();
        winnerHandIndex = 0;

        for (int i = 1; i < hands.Length; i++)
        {
        if (hands[i].Score > hands[winnerHandIndex].Score)
            {
                winnerHandIndex = i;
            }
        }


        endCardController.SetEndText(hands[winnerHandIndex].IsPlayer ? "Oyuncu Kazandı" : "Bilgisayar Kazandı");
        endCardController.ShowEndCard();
    }
}
