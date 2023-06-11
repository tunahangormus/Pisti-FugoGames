using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLogic
{
    public CardController PlayCard(List<CardController> cards, List<Card> playedCards, List<CardController> midPileCards)
    {

        CardController lastPlayedCard = null;

        if (midPileCards.Count > 0)
        {
            lastPlayedCard = midPileCards[midPileCards.Count - 1];
        }

        foreach (CardController card in cards)
        {
            if (lastPlayedCard != null && card.CardModel.Value == lastPlayedCard.CardModel.Value)
            {
                return card;
            }

            if (card.CardModel.Value == 11 && midPileCards.Count > 1)
            {
                return card;
            }
        }

        int maxPlayedCardCount = int.MinValue;
        CardController maxPlayedCard = null;

        foreach (CardController card in cards)
        {
            int cardPlayedCount = CountPlayedCard(card, playedCards);
            if (cardPlayedCount > maxPlayedCardCount)
            {
                maxPlayedCardCount = cardPlayedCount;
                maxPlayedCard = card;
            }
        }

        if (maxPlayedCard != null)
        {
            return maxPlayedCard;
        }

        return cards[Random.Range(0, cards.Count)];
    }

    private int CountPlayedCard(CardController card, List<Card> playedCards)
    {
        int count = 0;
        foreach (Card playedCard in playedCards)
        {
            if (playedCard.value == card.CardModel.Value)
            {
                count++;
            }
        }
        return count;
    }
}
