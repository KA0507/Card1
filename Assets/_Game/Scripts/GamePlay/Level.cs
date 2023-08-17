using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Character> bots = new List<Character>();
    public List<Character> players = new List<Character>();

    public void OnInit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(540, 1400, 0));
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LevelManager.Ins.groundLayer);
        LevelManager.Ins.botPosition.OnInit(hit.point);
        ray = Camera.main.ScreenPointToRay(new Vector3(540, 750, 0));
        Physics.Raycast(ray, out hit, Mathf.Infinity, LevelManager.Ins.groundLayer);
        LevelManager.Ins.playerPosition.OnInit(hit.point);

        for (int i = 0; i < Data.Ins.levelData.levelPlayers[LevelManager.Ins.indexLevel].players.Count; i++)
        {
            for (int j = 0; j < Data.Ins.levelData.levelPlayers[LevelManager.Ins.indexLevel].players[i].numberPlayer; j++)
            {
                LevelManager.Ins.CreatePlayer(Data.Ins.levelData.levelPlayers[LevelManager.Ins.indexLevel].players[i].skin);
            }
        }

        for (int i = 0; i < Data.Ins.levelData.levelBots[LevelManager.Ins.indexLevel].bots.Count; i++)
        {
            for (int j = 0; j < Data.Ins.levelData.levelBots[LevelManager.Ins.indexLevel].bots[i].numberBot; j++)
            {
                LevelManager.Ins.CreateBot(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            if (players.Count == 0)
            {
                LevelManager.Ins.Defeat();
            }
            if (bots.Count == 0)
            {
                LevelManager.Ins.Victory();
            }
        }       
    }
}
