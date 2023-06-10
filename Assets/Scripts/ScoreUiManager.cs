using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreUiManager
{
    // private static List<HandController> hands = new List<HandController>();
    private static HandController[] hands;

    public static void Init(HandController[] hands)
    {
        ScoreUiManager.hands = hands;
    }

    public static void UpdateScores()
    {
        foreach (HandController hand in hands)
        {
            string nameText = hand.IsPlayer ? "Oyuncu" : "Bilgisayar";
            hand.ScoreText.text = nameText + " | " + hand.Score.ToString();
        }
    }


}
