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
        //ChangeCard(cardType, type);
    }
    public void ChangeCard(CardType cardType, PoolType type)
    {
        this.cardType = cardType;
        this.type = type;
        switch (cardType)
        {
            case CardType.Digits:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Animals:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Spells:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Weapons:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Power_ups:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
            case CardType.Support:
                image.sprite = Data.Ins.cardData.GetData(cardType, type).image;
                break;
        }
    }
}

