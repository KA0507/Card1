using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelBot", menuName = "ScriptableObjects/LevelBot", order = 1)]
public class LevelData : ScriptableObject
{
    public List<LevelBots> levelBots;
    public List<LevelPlayers> levelPlayers;
}

[System.Serializable]
public class LevelBots
{
    public List<LevelBot> bots;
}

[System.Serializable]
public class LevelPlayers
{
    public List<LevelPlayer> players;
}

[System.Serializable]
public class LevelBot
{
    public int numberBot;
    public int hp;
    public int damage;
    public SkinType skin;
}

[System.Serializable]
public class LevelPlayer
{
    public int numberPlayer;
    public SkinType skin;
}
