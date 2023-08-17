using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCard : GameUnit
{
    public int numberCharacter;
    public bool isPlayer;
    public bool isBot;
    public UIGamePlay uiGamePlay;
    public SpriteRenderer renderer;
    public Material materialDefault;
    public void OnInit(UIGamePlay uIGamePlay , bool player, bool bot)
    {
        this.uiGamePlay = uIGamePlay;
        this.uiGamePlay.targets.Clear();
        renderer.material = materialDefault;
        isPlayer = player;
        isBot = bot;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character character = Cache.GetCharacter(other);
            if (isPlayer == true && character is Player)
            {
                renderer.material = LevelManager.Ins.materials[1];
                uiGamePlay.targets.Add(character);
            } else if (isBot == true && character is Bot)
            {
                renderer.material = LevelManager.Ins.materials[0];
                uiGamePlay.targets.Add(character);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character character = Cache.GetCharacter(other);
            if (isPlayer == true && character is Player)
            {
                uiGamePlay.targets.Remove(character);
                if (uiGamePlay.targets.Count == 0)
                {
                    renderer.material = materialDefault;
                }
            }
            else if (isBot == true && character is Bot)
            {
                uiGamePlay.targets.Remove(character);
                if (uiGamePlay.targets.Count == 0)
                {
                    renderer.material = materialDefault;
                }
            }
        }
    }
}
