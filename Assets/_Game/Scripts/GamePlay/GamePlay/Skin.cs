using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform AccessoryPosition;

    public Animator anim;
    public SkinType skinType;
    /*public int hp;
    public int damage;*/
    public Renderer renderer;
    public Weapon currentWeapon;
    public Accessory currentAccessory;

    public void OnInit()
    {
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }
        if (currentAccessory != null)
        {
            SimplePool.Despawn(currentAccessory);
            currentAccessory = null;
        }
    }
    public void ChanegeWeapon(Character c, WeaponType weaponType, bool upProperties = true)
    {
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, weaponPosition);
        if (upProperties)
        {
            c.hp += currentWeapon.hp;
            c.damage += currentWeapon.damage;
        }     
    }

    public void ChanegeAccessory(Character c, bool upProperties = true)
    {
        if (currentAccessory != null)
        {
            SimplePool.Despawn(currentAccessory);
        }
        currentAccessory = SimplePool.Spawn<Accessory>(PoolType.Power_Accessory, AccessoryPosition);
        if (upProperties)
        {
            c.hp += currentAccessory.hp;
            c.damage += currentAccessory.damage;
        }
    }
}
