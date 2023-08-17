using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public List<PlayerLevel> playerLevels;
}

[System.Serializable]
public class PlayerLevel
{
    public int coin;
    public int hp;
    public int damage;
}