using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField]
    private RectTransform playAreaRectTransform;


    public bool CheckCardIsInsidePlayArea(CardController cardController)
    {

        Vector2 cardPosition = cardController.CardRectTransform.position;
        Vector2 normalizedPosition = playAreaRectTransform.InverseTransformPoint(cardPosition);

        Vector2 playAreaSize = playAreaRectTransform.rect.size;
        Vector2 playAreaHalfSize = playAreaSize * 0.5f;

        normalizedPosition += playAreaHalfSize;

        normalizedPosition /= playAreaSize;

        if (normalizedPosition.x >= 0 && normalizedPosition.x <= 1 && normalizedPosition.y >= 0 && normalizedPosition.y <= 1)
        {
            return true;
        }
        return false;
    }

    public bool CheckCardInsideHandArea(CardController cardController)
    {
        if (playAreaRectTransform.position.y - (playAreaRectTransform.rect.size.y * 0.7) > cardController.CardRectTransform.position.y)
        {
            return true;
        }

        return false;
    }
}
