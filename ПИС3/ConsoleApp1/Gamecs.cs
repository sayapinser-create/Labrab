using System;
using System.Collections.Generic;

namespace пис3;

public class Game
{
    public List<Card> Deck = new List<Card>();
    public List<Card> Waste = new List<Card>();
    public List<Card>[] Tableau = new List<Card>[7];
    public List<Card>[] Foundations = new List<Card>[4];

    public Game()
    {
        for (int i = 0; i < 7; i++) Tableau[i] = new List<Card>();
        for (int i = 0; i < 4; i++) Foundations[i] = new List<Card>();
    }

    public void Start()
    {
    }

    public void DrawCard()
    {
    }

    public bool MoveCard(int fromType, int fromCol, int toType, int toCol, int startIndex)
    {
        return false;
    }

    public bool CheckRules(Card card, int toType, int toCol)
    {
        return false;
    }

    public void FlipTopCard(int col)
    {
    }

    public bool IsVictory()
    {
        return false;
    }
}