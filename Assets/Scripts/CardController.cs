using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class CardController : MonoBehaviour/* IPointerEnterHandler, IPointerExitHandler,*/ 
{
    [SerializeField]
    private CardView cardView;
    public CardView CardView { get { return cardView; } }

    [SerializeField]
    private RectTransform cardRectTransform;
    public RectTransform CardRectTransform { get { return cardRectTransform; } }

    private bool isInputEnabled = false;
    private HandController handController;
    // public HandController HandController { set { handController = value; } }

    [SerializeField]
    private CardInputController cardInputController;
    public CardInputController InputController { get { return cardInputController; } }


    private CardModel cardModel = new CardModel();
    public CardModel CardModel { get { return cardModel; } }

    
   

    private bool isOnHand = false;
    public bool IsOnHand { set { isOnHand = value; } get { return isOnHand; } }


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





    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     if (isInputEnabled && isOnHand)
    //     {
    //         isOnHand = false;
    //         Vector3 pos = this.transform.localPosition;
    //         pos.y = cardRectTransform.rect.height * gameObject.transform.localScale.y * 0.3f;
    //         this.transform.DOLocalMove(pos, 0.2f).SetEase(Ease.OutQuint).OnComplete(() =>
    //         {
    //             isOnHand = true;
    //         });
    //         // this.transform.localPosition = pos;
    //     }
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if (isInputEnabled && isOnHand)
    //     {
    //         isOnHand = false;
    //         Vector3 pos = this.transform.localPosition;
    //         pos.y = 0;
    //         this.transform.DOLocalMove(pos, 0.2f).SetEase(Ease.OutQuint).OnComplete(() =>
    //         {
    //             isOnHand = true;
    //         });
    //     }
    //     // if (isInputEnabled)
    //     // {
    //     //     Vector3 pos = this.transform.localPosition;
    //     //     pos.y = 0;
    //     //     this.transform.DOLocalMove(pos, 0.2f).SetEase(Ease.OutQuint);

    //     //     // handController.PositionCards();
    //     // }
    // }

}
