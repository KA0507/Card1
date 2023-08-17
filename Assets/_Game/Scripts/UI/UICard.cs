using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICard : UICanvas
{
    public List<ButtonChangeCard> buttons;

    public RectTransform powerup;
    public RectTransform animal;
    public RectTransform spell;
    public RectTransform weapon;
    public RectTransform support;
    public RectTransform digit;

    public ButtonCardInUICard cardSelected;
    public bool isClick;
    public bool hasCard;

    private void Awake()
    {
        int n = Data.Ins.cardData.powerupCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.powerupCards[i].cardType, Data.Ins.cardData.powerupCards[i].poolType);
            buttonCard.transform.SetParent(powerup);
        }

        n = Data.Ins.cardData.animalCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.animalCards[i].cardType, Data.Ins.cardData.animalCards[i].poolType);
            buttonCard.transform.SetParent(animal);
        }
        
        n = Data.Ins.cardData.weaponCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.weaponCards[i].cardType, Data.Ins.cardData.weaponCards[i].poolType);
            buttonCard.transform.SetParent(weapon);
        }

        n = Data.Ins.cardData.digitCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.digitCards[i].cardType, Data.Ins.cardData.digitCards[i].poolType);
            buttonCard.transform.SetParent(digit);
        }
        
        n = Data.Ins.cardData.supportCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.supportCards[i].cardType, Data.Ins.cardData.supportCards[i].poolType);
            buttonCard.transform.SetParent(support);
        }
        
        n = Data.Ins.cardData.spellCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCardInUICard buttonCard = SimplePool.Spawn<ButtonCardInUICard>(PoolType.ButtonCardInUICard);
            buttonCard.OnInit(this);
            buttonCard.ChangeCard(Data.Ins.cardData.spellCards[i].cardType, Data.Ins.cardData.spellCards[i].poolType);
            buttonCard.transform.SetParent(spell);
        }
    }
    private void Update()
    {
        if (isClick && !hasCard)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("mouse");
                cardSelected = null;
                isClick = false;
            }
        }
    }

    public void OpenUICard()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ChangeCard(UserData.Ins.GetEnumData<CardType>(UserData.KEY_BUTTON_CARDTYPE + i, CardType.Digits),
                                  UserData.Ins.GetEnumData<PoolType>(UserData.KEY_BUTTON_POOLTYPE + i, PoolType.None));
        }
    }

    public void OpenUIMainMenu()
    {
        UIManager.Ins.CloseUI<UICard>();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
    public void SetCard()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            UserData.Ins.SetEnumData<CardType>(UserData.KEY_BUTTON_CARDTYPE + i, buttons[i].cardType);
            UserData.Ins.SetEnumData<PoolType>(UserData.KEY_BUTTON_POOLTYPE + i, buttons[i].type);
        }
    }
    public void ChooseCard(ButtonCardInUICard buttonCard)
    {
        //Debug.Log("click");
        isClick = true;
        cardSelected = buttonCard;
    }
}
