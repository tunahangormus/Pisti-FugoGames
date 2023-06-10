using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandPositioner
{

    private float tweenDuration = 0.7f;
    public float PositionCards(List<CardController> cards, float delay)
    {
        float currentDelay = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            CardController cardController = cards[i];
            // float cardWidth = cardController.transform.localScale.x * cardController.CardRectTransform.rect.width * 1.1f;
            // float xPos = -cards.Count / 2 * cardWidth + cardWidth / 2 + i * cardWidth;
            float xPos = CalculatePosition(cards, cardController);
            // float angle = -(i - (cards.Count - 1) / 2f) * 10;

            // cardController.transform.localPosition = new Vector3(xPos, 0, 0);
            // cardController.transform.localRotation = Quaternion.Euler(0, 0, angle);

            if (cardController.IsOnHand == false)
            {
                // cardController.transform.DOKill();
                cardController.transform.DOLocalMove(new Vector3(xPos, 0, 0), tweenDuration).SetDelay(currentDelay).OnComplete(() =>
                {
                    cardController.IsOnHand = true;
                });

                if (i < cards.Count - 1)
                {
                    currentDelay += delay;
                }
                // cardController.transform.DOLocalRotate(new Vector3(0, 0, angle), 0.5f);
            }
        }

        return tweenDuration + currentDelay;
    }

    public void PositionCard(List<CardController> cards, CardController cardController)
    {
        float xPos = CalculatePosition(cards, cardController);
        if (cardController.IsOnHand == false)
        {
            cardController.transform.DOKill();
            cardController.transform.DOLocalMove(new Vector3(xPos, 0, 0), 0.35f).OnComplete(() =>
            {
                cardController.IsOnHand = true;
            });
        }
    }

    public float CalculatePosition(List<CardController> cards, CardController card)
    {
        float cardWidth = card.transform.localScale.x * card.CardRectTransform.rect.width * 1.1f;
        float xPos = -cards.Count / 2 * cardWidth + cards.IndexOf(card) * cardWidth;
        if (cards.Count % 2 == 0)
        {
            xPos += cardWidth / 2;
        }
        return xPos;
    }


}
