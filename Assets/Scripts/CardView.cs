using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardView : MonoBehaviour
{

    [SerializeField]
    private Image spriteRenderer;

    [SerializeField]
    private TextMeshProUGUI valueText;

    private Color blackColor = new Color(51f / 255f, 54f / 255f, 88f / 255f);
    private Color redColor = new Color(213f / 255f, 82f / 255f, 141f / 255f);

    private Sprite cardFrontSprite;
    private Sprite cardBackSprite;

    private bool isFaceUp;
    public void SetCardValue(int value, Suit suit)
    {
        string suitName = suit.ToString().ToLower();
        string path;
        if (value > 10)
        {
            string valueString = ((Value)(value - 11)).ToString().ToLower();
            path = "Sprites/" + valueString + "_" + suitName;
            valueText.text = "";
        }
        else
        {
            path = "Sprites/" + suitName;
            if (value == 1)
            {
                valueText.text = "A";
            }
            else
            {
                valueText.text = value.ToString();
            }
            valueText.color = (suit == Suit.Spade || suit == Suit.Club) ? blackColor : redColor;
        }

        cardFrontSprite = Resources.Load<Sprite>(path);
        cardBackSprite = Resources.Load<Sprite>("Sprites/cardBack");

        valueText.enabled = false;
        isFaceUp = false;
        spriteRenderer.sprite = cardBackSprite;
    }

    public void SetCardFace(bool isFaceUp, float delay = 0)
    {
        if (isFaceUp == this.isFaceUp)
        {
            return;
        }

        float xScale = transform.localScale.x;
        float tweenDuration = 0.15f;
        transform.DOScaleX(0, tweenDuration).SetDelay(delay).OnComplete(() =>
        {
            this.isFaceUp = isFaceUp;
            spriteRenderer.sprite = isFaceUp ? cardFrontSprite : cardBackSprite;
            valueText.enabled = isFaceUp;
            spriteRenderer.transform.DOScaleX(xScale, tweenDuration);
        });

    }


}
