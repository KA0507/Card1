using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Material> materials;
    public List<Level> levels;
    public LayerMask groundLayer;
    public Level currentLevel;
    public GridPosition playerPosition;
    public GridPosition botPosition;
    public int indexLevel;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = new GridPosition();
        botPosition = new GridPosition();
        indexLevel = 0;
        currentLevel = Instantiate(levels[indexLevel]);
        currentLevel.OnInit();
    }

    public void OnInit()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameManager.Ins.isState(GameState.GAMEPLAY))
        {*/
            
        //}
    }

    public void NextLevel()
    {
        indexLevel++;
    }

    // Tải level
    public void LoadLevel()
    {
        for (int i = currentLevel.players.Count; i > 0; i--)
        {
            currentLevel.players[i - 1].Despawn();
            currentLevel.players.Remove(currentLevel.players[i-1]);
        }        
        currentLevel.players.Clear();
        for (int i = currentLevel.bots.Count; i > 0; i--)
        {
            currentLevel.bots[i-1].Despawn();
            currentLevel.bots.Remove(currentLevel.bots[i-1]);
        }
        currentLevel.bots.Clear();
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[indexLevel]);
        currentLevel.OnInit();
    }

    public void Victory()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIWin>();
        GameManager.Ins.ChangeState(GameState.FINISH);
        //player.ChangeAnim(Constant.ANIM_WIN);
        NextLevel();
    }

    // Thua game
    public void Defeat()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UILose>();
        GameManager.Ins.ChangeState(GameState.FINISH);
    }

    public void CreatePlayer()
    {
        Player player = SimplePool.Spawn<Player>(PoolType.Player, playerPosition.point, Quaternion.identity);
        player.OnInit();
        player.currentSkin.renderer.material = LevelManager.Ins.materials[1];
        player.TF.DOLocalMove(playerPosition.gridPositions[0], 1f);
        playerPosition.gridPositions.RemoveAt(0);
        currentLevel.players.Add(player);
    }

    public void CreatePlayer(Character character)
    {
        Player player = SimplePool.Spawn<Player>(PoolType.Player, playerPosition.point, Quaternion.identity);
        player.OnInit(character);
        player.currentSkin.renderer.material = LevelManager.Ins.materials[1];
        player.TF.DOLocalMove(playerPosition.gridPositions[0], 1f);
        playerPosition.gridPositions.RemoveAt(0);
        currentLevel.players.Add(player);
    }

    public void CreateBot()
    {
        /*Ray ray = Camera.main.ScreenPointToRay(new Vector3(540, 1300, 0));
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer);*/
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot);
        bot.OnInit();
        bot.TF.localPosition = botPosition.gridPositions[0];
        botPosition.gridPositions.RemoveAt(0);
        bot.currentSkin.renderer.material = LevelManager.Ins.materials[0];
        currentLevel.bots.Add(bot);

        //bot.TF.DOLocalMove(GridPosition.Ins.gridPositions[0], 1f);
        //GridPosition.Ins.gridPositions.RemoveAt(0);
    }
}
