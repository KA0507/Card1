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
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }
}
