using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class CardController : MonoBehaviour
{
    [SerializeField]
    private CardView cardView;
    public CardView CardView { get { return cardView; } }

    [SerializeField]
    private RectTransform cardRectTransform;
    public RectTransform CardRectTransform { get { return cardRectTransform; } }
    private HandController handController;

    [SerializeField]
    private CardInputController cardInputController;
    public CardInputController InputController { get { return cardInputController; } }


    private CardModel cardModel = new CardModel();
    public CardModel CardModel { get { return cardModel; } }

    
   

    private bool isOnHand = false;
    public bool IsInPosition { set { isOnHand = value; } get { return isOnHand; } }


    public void SetCardValue(int value, Suit suit)
    {
        cardView.SetCardValue(value, suit);
        cardModel.SetCardValue(value, suit);
    }

    public void SetHandController(HandController handController)
    {
        this.handController = handController;
        cardInputController.HandController = handController;
    }
}
