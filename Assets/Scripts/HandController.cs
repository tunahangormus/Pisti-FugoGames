using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class HandController : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private List<CardController> cards = new List<CardController>();
    public List<CardController> Cards { get { return cards; } }

    private HandPositioner handPositioner = new HandPositioner();

    private CardController selectedCard;
    public CardController SelectedCard { set { selectedCard = value; } get { return selectedCard; } }

    [SerializeField]
    private AreaController areaController;

    private GameController gameController;
    public GameController GameController { set { gameController = value; } }

    [SerializeField]
    private GameObject stash;
    private int stashCount = 0;
    public int StashCount { get { return stashCount; } }
    private List<CardController> stashCards = new List<CardController>();
    public List<CardController> StashCards { get { return stashCards; } }

    [SerializeField]
    private TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    private int score = 0;
    public int Score { get { return score; } }

    public bool IsPlayer { get { return isPlayer; } }


    public void AddCard(CardController card)
    {
        card.transform.SetParent(transform);
        card.SetHandController(this);
        cards.Add(card);
    }

    public void RemoveCard(CardController card)
    {
        cards.Remove(card);
    }

    public float PositionCards(float delay = 0)
    {
        return handPositioner.PositionCards(cards, delay);
    }

    public void EnableInput(CardController card)
    {
        card.InputController.EnableInput();
    }

    public void EnableInputForAllCards()
    {
        foreach (CardController card in cards)
        {
            card.InputController.EnableInput();
        }
    }

    public void DisableInputForAllCards()
    {
        foreach (CardController card in cards)
        {
            card.InputController.DisableInput();
        }
    }

    public void ArrangeCards(CardController selectedCard)
    {
        if (!areaController.CheckCardInsideHandArea(selectedCard))
        {
            return;
        }

        int cardIndex = cards.IndexOf(selectedCard);
        if (cardIndex > 0 && selectedCard.transform.localPosition.x < cards[cardIndex - 1].transform.localPosition.x)
        {
            cards.Remove(selectedCard);
            cards.Insert(cardIndex - 1, selectedCard);
            cards[cardIndex].IsOnHand = false;
            handPositioner.PositionCard(cards, cards[cardIndex]);
        }
        else if (cardIndex < cards.Count - 1 && selectedCard.transform.localPosition.x > cards[cardIndex + 1].transform.localPosition.x)
        {
            cards.Remove(selectedCard);
            cards.Insert(cardIndex + 1, selectedCard);
            cards[cardIndex].IsOnHand = false;
            handPositioner.PositionCard(cards, cards[cardIndex]);

        }
    }

    public void PointerDown(CardController card)
    {
        card.transform.SetAsLastSibling();
    }

    public void PlayCard(CardController card)
    {
        card.IsOnHand = false;
        RemoveCard(card);
        card.InputController.DisableInput();
    }

    public void DragEnd(CardController card)
    {
        bool isDroppedOnPlayArea = areaController.CheckCardIsInsidePlayArea(card);
        if (isDroppedOnPlayArea)
        {

            StartCoroutine(gameController.CardPlayed(card));
            PlayCard(card);
            foreach (CardController c in cards)
            {
                c.IsOnHand = false;
            }
        }
        PositionCards();
    }

    public float CollectCardsToStash(List<CardController> pileCards)
    {
        float tweenDuration = 0.75f;
        float delay = 0;
        foreach (CardController card in pileCards)
        {
            card.transform.SetParent(stash.transform);
            card.transform.DOLocalMove(Vector3.zero, tweenDuration).SetDelay(delay);
            delay += 0.05f;
            stashCards.Add(card);
            stashCount++;
        }

        return tweenDuration + delay;

    }

    public void emptyStash()
    {
        stashCards.Clear();
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void ResetScore()
    {
        this.score = 0;
    }
}
