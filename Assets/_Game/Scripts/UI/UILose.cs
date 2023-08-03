using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UICanvas
{
    public void MainMenu()
    {
        UIManager.Ins.CloseUI<UILose>();
        UIManager.Ins.OpenUI<UIMainMenu>().OpenMainMenu();
        LevelManager.Ins.LoadLevel();
    }
}
