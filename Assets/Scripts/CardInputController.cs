using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{

    private HandController handController;
    public HandController HandController { set { handController = value; } }

    [SerializeField]
    private CardController cardController;
    private bool isInputEnabled = false;

    public void EnableInput()
    {
        isInputEnabled = true;
    }

    public void DisableInput()
    {
        isInputEnabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInputEnabled)
        {
            handController.PointerDown(cardController);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isInputEnabled && cardController.IsInPosition)
        {
            handController.ArrangeCards(cardController);
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isInputEnabled)
        {
            cardController.IsInPosition = false;
            handController.DragEnd(cardController);
        }
    }
}
