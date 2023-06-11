
public enum Suit
{
    Spade, Heart, Diamond, Club
}

public enum Value
{
    Jack, Queen, King
}

public class CardData
{
    public int value { get; }
    public Suit suit { get; }

    public CardData(int value, Suit suit)
    {
        this.value = value;
        this.suit = suit;
    }


}