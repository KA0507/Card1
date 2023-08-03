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
    public int hp;
    public int damage;
    public Renderer renderer;
    public Weapon currentWeapon;
    public Accessory currentAccessory;

    public void ChanegeWeapon(WeaponType weaponType)
    {
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, weaponPosition);
        hp += currentWeapon.hp;
        damage += currentWeapon.damage;
    }

    public void ChanegeAccessory(AccessoryType accessoryType)
    {
        if (currentAccessory != null)
        {
            hp -= currentAccessory.hp;
            SimplePool.Despawn(currentAccessory);
        }
        SimplePool.Spawn<Weapon>((PoolType)accessoryType, AccessoryPosition);
        hp += currentAccessory.hp;
    }
}
