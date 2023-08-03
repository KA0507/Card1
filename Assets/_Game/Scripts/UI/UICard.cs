using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICard : UICanvas
{
    public List<ButtonCard> buttons;

    public RectTransform powerup;
    public RectTransform animal;
    public RectTransform spell;
    public RectTransform weapon;
    public RectTransform support;
    public RectTransform digit;

    public ButtonCard cardSelected;

    private void Awake()
    {
        int n = Data.Ins.cardData.digitCards.Count;
        for (int i = 0; i < n; i++)
        {
            ButtonCard button = SimplePool.Spawn<ButtonCard>(PoolType.ButtonCard);
            button.ChangeCard(Data.Ins.cardData.digitCards[i].cardType, Data.Ins.cardData.digitCards[i].poolType);
            button.transform.SetParent(powerup);
            button.btn.onClick.RemoveAllListeners();
            button.btn.onClick.AddListener(() => button.ChooseCardReplace(this));
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

    public void SetCard()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            UserData.Ins.SetEnumData<CardType>(UserData.KEY_BUTTON_CARDTYPE + i, buttons[i].cardType);
            UserData.Ins.SetEnumData<PoolType>(UserData.KEY_BUTTON_POOLTYPE + i, buttons[i].type);
        }
    }
    public void ChooseTypeCard(ButtonCard card)
    {
        cardSelected = card;
    }
}
