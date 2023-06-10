using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public enum Suit
{
    Spade, Heart, Diamond, Club
}

public enum Value
{
    Jack, Queen, King
}

public class CardDeck
{
    private List<Card> deck;
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
            Card temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
        currentCardIndex = 0;
    }

    public Card DrawCard()
    {
        if (currentCardIndex >= deck.Count)
        {
            throw new InvalidOperationException("No more cards in the deck. Shuffle the deck to start again.");
        }

        Card drawnCard = deck[currentCardIndex];
        currentCardIndex++;
        return drawnCard;
    }

    private List<Card> GenerateDeck()
    {
        List<Card> newDeck = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                Card newCard = new Card(rank, suit);
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