using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLogic
{
    public CardController PlayCard(List<CardController> cards, List<CardController> playedCards)
    {
        CardController lastPlayedCard = playedCards[playedCards.Count - 1];
        foreach (CardController card in cards)
        {
            if (card.CardModel.Value == lastPlayedCard.CardModel.Value)
            {
                return card;
            }

            if (card.CardModel.Value == 11)
            {
                return card;
            }
        }

        
        return cards[Random.Range(0, cards.Count)];
    }
}
