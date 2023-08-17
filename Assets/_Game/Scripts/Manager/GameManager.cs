using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MAINMENU, GAMEPLAY, FINISH}
public class GameManager : Singleton<GameManager>
{
    public GameState state;

    public bool IsState(GameState gameState) => gameState == state;

    private void Awake()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        /*for (int i = 0; i < 5; i++)
        {
            UserData.Ins.SetEnumData(UserData.KEY_BUTTON_CARDTYPE + i, CardType.Digits);
            UserData.Ins.SetEnumData(UserData.KEY_BUTTON_POOLTYPE + i, PoolType.Digit_Add1);
        }*/
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }
}
