using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public void MainMenu()
    {
        UIManager.Ins.CloseUI<UIWin>();
        UIManager.Ins.OpenUI<UIMainMenu>().OpenMainMenu();
        LevelManager.Ins.LoadLevel();
    }
}
