using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Character> bots = new List<Character>();
    public List<Character> players = new List<Character>();

    public int nPlayer;
    public int nBot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(540, 1300, 0));
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LevelManager.Ins.groundLayer);
        LevelManager.Ins.botPosition.OnInit(hit.point);
        ray = Camera.main.ScreenPointToRay(new Vector3(540, 600, 0));
        Physics.Raycast(ray, out hit, Mathf.Infinity, LevelManager.Ins.groundLayer);
        LevelManager.Ins.playerPosition.OnInit(hit.point);

        for (int i = 0; i < nPlayer; i++)
        {
            LevelManager.Ins.CreatePlayer();
        }

        for (int i = 0; i < nBot; i++)
        {
            LevelManager.Ins.CreateBot();
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
