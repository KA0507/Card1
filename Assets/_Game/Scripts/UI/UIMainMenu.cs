using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    public Text textLevel;

    public void OpenMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MAINMENU);
        textLevel.text = Constant.LEVEL + (LevelManager.Ins.indexLevel + 1);
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
