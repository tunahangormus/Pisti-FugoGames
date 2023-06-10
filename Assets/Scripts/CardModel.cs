using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{

    private int value;
    public int Value { get { return value; } }
    private Suit suitName;
    public Suit SuitName { get { return suitName; } }


    public void SetCardValue(int value, Suit suitName)
    {
        this.value = value;
        this.suitName = suitName;
    }
}
