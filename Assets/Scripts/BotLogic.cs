using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLogic
{
    public CardController PlayCard(List<CardController> cards, List<Card> playedCards)
    {
        Card lastPlayedCard = playedCards[playedCards.Count - 1];
        foreach (CardController card in cards)
        {
            if (card.CardModel.Value == lastPlayedCard.value)
            {
                return card;
            }

            if (card.CardModel.Value == 11 && playedCards.Count > 1)
            {
                return card;
            }
        }


        return cards[Random.Range(0, cards.Count)];
    }
}
