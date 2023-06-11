using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController
{
    private HandController[] hands;

    public void Init(HandController[] hands)
    {
        this.hands = hands;
        UpdateScores();
    }

    public void UpdateScores()
    {
        foreach (HandController hand in hands)
        {
            string nameText = hand.IsPlayer ? "Oyuncu" : "Bilgisayar";
            hand.ScoreText.text = nameText + " | " + hand.Score.ToString();
        }
    }
}
