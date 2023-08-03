using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class LevelPlayerData : ScriptableObject
{
    public List<PlayerLevel> playerLevels;
}

public class PlayerLevel
{
    public int hp;
    public int damage;
}