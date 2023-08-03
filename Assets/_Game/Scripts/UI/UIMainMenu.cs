using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    

    public void OpenMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MAINMENU);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUICard()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UICard>().OpenUICard();
    }
    public void SelectCard()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIGamePlay>().UseCard();

    }
}
