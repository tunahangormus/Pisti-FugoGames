using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MidPileController : MonoBehaviour
{
    private List<CardController> cards = new List<CardController>();
    public List<CardController> Cards { get { return cards; } }

    private int currentScore = 0;
    public int CurrentScore { get { return currentScore; } }

    public float AddCard(CardController cardController, float delay = 0)
    {
        cards.Add(cardController);
        cardController.transform.SetParent(transform);
        Vector3 cardPosition = Vector3.zero;

        if (transform.childCount > 1)
        {
            cardPosition.x = Random.Range(30f, -30f);
            cardPosition.y = Random.Range(30f, -30f);
        }

        float tweenDuration = 0.4f;
        cardController.transform.DOLocalMove(cardPosition, tweenDuration).SetDelay(delay).SetEase(Ease.InOutSine);

        return tweenDuration + delay;
    }

    public List<CardController> GetCardsAndEmptyPile()
    {
        List<CardController> cardsToReturn = new List<CardController>();
        cardsToReturn.AddRange(cards);
        cards.Clear();
        return cardsToReturn;
    }

    public void UpdateScore()
    {
        currentScore = 0;

        foreach (CardController card in cards)
        {
            if (card.CardModel.Value == 1)
            {
                currentScore += 1;
            }
            else if (card.CardModel.Value == 2 && card.CardModel.SuitName == Suit.Club)
            {
                currentScore += 2;
            }
            else if (card.CardModel.Value == 10 && card.CardModel.SuitName == Suit.Diamond)
            {
                currentScore += 3;
            }
            else if (card.CardModel.Value == 11)
            {
                currentScore += 1;
            }
        }

        if (cards.Count == 2 && cards[0].CardModel.Value == cards[1].CardModel.Value)
        {
            currentScore += 10;
        }

    }

}
