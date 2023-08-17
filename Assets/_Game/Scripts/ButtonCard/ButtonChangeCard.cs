using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonChangeCard : ButtonCard, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public UICard uiCard;
    public int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiCard.cardSelected != null)
        {
            ChangeCard(uiCard.cardSelected.cardType, uiCard.cardSelected.type);
            uiCard.SetCard();
            uiCard.cardSelected = null;
            uiCard.isClick = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        uiCard.hasCard = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        uiCard.hasCard = false;
    }
}
