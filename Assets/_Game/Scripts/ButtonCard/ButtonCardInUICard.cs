using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCardInUICard : ButtonCard, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public UICard uiCard;

    public void OnInit(UICard uICard)
    {
        this.uiCard = uICard;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiCard.ChooseCard(this);
    }

    /*public void ChangeCardInUICard(UICard uiCard)
    {
        if (uiCard.cardSelected != null)
        {
            ChangeCard(uiCard.cardSelected.cardType, uiCard.cardSelected.type);
            uiCard.SetCard();
            uiCard.cardSelected = null;
        }
    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        uiCard.hasCard = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        uiCard.hasCard = false;
    }

    /*public void ChooseCardReplace(UICard uiCard)
    {
        uiCard.ChooseCard(this);
    }*/

}
