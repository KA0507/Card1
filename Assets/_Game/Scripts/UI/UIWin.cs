using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public RotateUIAds rotateUIAds;
    public int coinWin;

    
    public void MainMenu()
    {
        UIManager.Ins.CloseUI<UIWin>();
        UIManager.Ins.OpenUI<UIMainMenu>().OpenMainMenu();
        LevelManager.Ins.LoadLevel();
        PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN, 0) + 50);
    }

    public void Claim()
    {
        UIManager.Ins.CloseUI<UIWin>();
        UIManager.Ins.OpenUI<UIMainMenu>().OpenMainMenu();
        LevelManager.Ins.LoadLevel();
        PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN, 0) + coinWin);
    }
}
