using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MidPileController : MonoBehaviour
{
    private List<CardController> cards = new List<CardController>();
    public List<CardController> Cards { get { return cards; }}

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

        float tweenDuration = 0.5f;
        cardController.transform.DOLocalMove(cardPosition, tweenDuration).SetDelay(delay).SetEase(Ease.InOutSine);

        return tweenDuration + delay;
    }

    public List<CardController> GetCardsAndEmptyPile(){
        List<CardController> cardsToReturn = new List<CardController>();
        cardsToReturn.AddRange(cards);
        cards.Clear();
        return cardsToReturn;
    }

}
