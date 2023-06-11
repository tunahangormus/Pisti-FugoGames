using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CardDeck
{
    private List<CardData> deck;
    private int currentCardIndex;

    public CardDeck()
    {
        CreateDeck();
    }

    public void CreateDeck()
    {
        deck = GenerateDeck();
        Shuffle();
    }

    public void Shuffle()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            CardData temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
        currentCardIndex = 0;
    }

    public CardData DrawCard()
    {
        if (currentCardIndex >= deck.Count)
        {
            throw new InvalidOperationException("No more cards in the deck.");
        }

        CardData drawnCard = deck[currentCardIndex];
        currentCardIndex++;
        return drawnCard;
    }

    private List<CardData> GenerateDeck()
    {
        List<CardData> newDeck = new List<CardData>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                CardData newCard = new CardData(rank, suit);
                newDeck.Add(newCard);
            }
        }
        

        return newDeck;
    }

    public int RemainingCardsLength()
    {
        return deck.Count - currentCardIndex;
    }
}