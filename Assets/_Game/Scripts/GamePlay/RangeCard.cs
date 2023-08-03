using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCard : GameUnit
{
    public int numberCharacter;
    public bool isPlayer;
    public bool isBot;
    public UIGamePlay uiGamePlay;
    public void OnInit(UIGamePlay uIGamePlay , bool player, bool bot)
    {
        this.uiGamePlay = uIGamePlay;
        this.uiGamePlay.targets.Clear();
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
                uiGamePlay.targets.Add(character);
            } else if (isBot == true && character is Bot)
            {
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
            }
            else if (isBot == true && character is Bot)
            {
                uiGamePlay.targets.Remove(character);
            }
        }
    }
}
