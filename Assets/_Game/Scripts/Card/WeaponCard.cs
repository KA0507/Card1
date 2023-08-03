using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card
{
    public WeaponType weaponType;
}

public enum WeaponType
{
    Sword = PoolType.Weapon_Sword,
    Bow = PoolType.Weapon_Bow,
    Gun = PoolType.Weapon_Gun,
    Hammer = PoolType.Weapon_Hammer
}
