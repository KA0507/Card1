using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    public List<DataSkin> dataSkins;

    public DataSkin GetDataSkin(SkinType skinType)
    {
        return dataSkins.Single(q => q.skinType == skinType);
    }
}

[System.Serializable]
public class DataSkin
{
    public SkinType skinType;
    public int hp;
    public int damage;
}
