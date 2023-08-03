using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCard : GameUnit
{
    public Button btn;
    public RectTransform rect;
    public PoolType type;
    public CardType cardType;
    public Image image;

    private void Awake()
    {
        ChangeCard(cardType, type);
    }
    public void ChangeCard(CardType cardType, PoolType type)
    {
        this.cardType = cardType;
        this.type = type;
        //image.sprite = data.GetData(cardType, type).image;
        switch (cardType)
        {
            case CardType.Digits:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Animals:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;

        }
    }

    public void ChangeCardInUICard(UICard uiCard)
    {
        if (uiCard.cardSelected != null)
        {
            /*Debug.Log(uiCard.cardSelected.cardType.ToString());
            Debug.Log(uiCard.cardSelected.poolType.ToString());*/
            ChangeCard(uiCard.cardSelected.cardType, uiCard.cardSelected.type);
            uiCard.SetCard();
            uiCard.cardSelected = null;
        }
    }

    public void SelectCard(UIGamePlay uiGamePlay, int n)
    {
        uiGamePlay.SelectCard(this, n);

        /*rect.localPosition = new Vector3(rect.localPosition.x, -300, rect.localPosition.z);
        rect.DOLocalRotate(Vector3.zero, 1f);
        uiGamePlay.currentCard = (Card)Data.Ins.cardData.GetData(cardType, type);
        uiGamePlay.hasCard = true;
        uiGamePlay.currentButtonCard = this;*/
    }
    
    public void ChooseCardReplace(UICard uiCard)
    {
        uiCard.ChooseTypeCard(this);
    }
}

