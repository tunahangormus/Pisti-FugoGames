public class Card
{
    public int rank { get; }
    public Suit suit { get; }

    public Card(int rank, Suit suit)
    {
        this.rank = rank;
        this.suit = suit;
    }

    public override string ToString()
    {
        return $"{rank} of {suit}";
    }
}