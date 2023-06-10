using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardPool : MonoBehaviour
{
    private Stack<CardController> cards = new Stack<CardController>();
    [SerializeField]
    private CardController cardPrefab;

    public void createPool()
    {
        for (int i = 0; i < 52; i++)
        {
            CardController card = Instantiate(cardPrefab, transform.position, Quaternion.identity, transform);
            card.gameObject.SetActive(false);
            cards.Push(card);
        }
    }

    public CardController getCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }
        CardController card = cards.Pop();
        return card;
    }

    public void returnCard(CardController card)
    {
        card.gameObject.SetActive(false);
        card.transform.position = transform.position;
        card.transform.SetParent(transform);
        cards.Push(card);
    }
}
