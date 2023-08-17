using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PoolControler : MonoBehaviour
{
    [Header("---- POOL CONTROLER TO INIT POOL ----")]
    //[Header("Put object pool to list Pool or Resources/Pool")]
    //[Header("Preload: Init Poll")]
    //[Header("Spawn: Take object from pool")]
    //[Header("Despawn: return object to pool")]
    //[Header("Collect: return objects type to pool")]
    //[Header("CollectAll: return all objects to pool")]

    [Space]
    [Header("Pool")]
    public List<PoolAmount> Pool;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            SimplePool.Preload(Pool[i].prefab, Pool[i].amount, Pool[i].root, Pool[i].collect);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(PoolControler))]
public class PoolControlerEditor : Editor
{
    PoolControler pool;

    private void OnEnable()
    {
        pool = (PoolControler)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Quick Root"))
        {
            for (int i = 0; i < pool.Pool.Count; i++)
            {
                if (pool.Pool[i].root == null)
                {
                    Transform tf = new GameObject(pool.Pool[i].prefab.poolType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Pool[i].root = tf; 
                }
            }
            
            for (int i = 0; i < pool.Particle.Length; i++)
            {
                if (pool.Particle[i].root == null)
                {
                    Transform tf = new GameObject(pool.Particle[i].particleType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Particle[i].root = tf; 
                }
            }
        }

        if (GUILayout.Button("Get Prefab Resource"))
        {
            GameUnit[] resources = Resources.LoadAll<GameUnit>("Pool");

            for (int i = 0; i < resources.Length; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < pool.Pool.Count; j++)
                {
                    if (resources[i].poolType == pool.Pool[j].prefab.poolType)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    Transform root = new GameObject(resources[i].name).transform;

                    PoolAmount newPool = new PoolAmount(root, resources[i], SimplePool.DEFAULT_POOL_SIZE, true);

                    pool.Pool.Add(newPool);
                }
            }
        }
    }
}

#endif

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;

    public PoolAmount (Transform root, GameUnit prefab, int amount, bool collect)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
        this.collect = collect;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    Hit,
    Level_up

}

public enum PoolType
{
    None = 0,

    Player = 1,
    Bot = 2,

    Digit_Add1 = 10,
    Digit_Add3 = 11,
    Digit_Add5 = 12,
    Digit_Add7 = 13,
    Digit_Sub3 = 14,
    Digit_Mul2 = 15,
    Digit_Mul3 = 16,
    Digit_Mul4 = 17,
    Digit_Mul5 = 18,
    Digit_Div2 = 19,
    Digit_Div3 = 20,

    Skin_Normal_Player = 50,
    Skin_Normal_Bot = 51,
    Skin_Chicken = 52,
    Skin_Dino = 53,
    Skin_Scorpion = 54,
    Skin_Spider = 55,
    Skin_Bear = 56,
    Skin_Wolf = 57,
    Skin_Mammouth = 58,
    Skin_Dragon = 59,
    Skin_Bot_Bow = 60,


    Weapon_Sword = 300,
    Weapon_Bow = 301,
    Weapon_Gun = 302,
    Weapon_Hammer = 303,

    Power_Accessory = 350,
    Power_UpSize = 351,
    Power_Flash = 352,
    Power_UpHp = 353,
    Power_Water = 354,

    Support_Cannon = 400,
    Support_Crossbow = 401,
    Support_IronClamp = 402,
    Support_Vines = 403,
    Support_Bomb = 404,

    Bullet_Cannon = 450,
    Bullet_Crossbow = 451,
    Bullet_Bow = 452,
    Bullet_Gun = 453,

    Spell_FireBall = 500,
    Spell_UFO = 501,
    Spell_Poison = 502,
    Spell_ZoomOut = 503,
    Spell_Balloon = 504,
    Spell_Ally = 505,
    Spell_Freeze = 506,
    Spell_Sub50Percent = 507,



    RangeCard = 900,



    ButtonCard = 950,
    ButtonCardInUICard = 960,

    TargetIndicator = 1000
}


