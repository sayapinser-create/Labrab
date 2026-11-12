namespace пис3;

public class Card
{
    public string Suit;
    public int Rank;
    public bool IsFaceUp;

    public Card(string suit, int rank)
    {
        Suit = suit;
        Rank = rank;
        IsFaceUp = false;
    }

    public bool IsRed()
    {
        return false;
    }
}