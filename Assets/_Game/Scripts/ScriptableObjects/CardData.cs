using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.MaterialProperty;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public List<DigitCard> digitCards;
    public List<AnimalCard> animalCards;
    public List<WeaponCard> weaponCards;
    public List<SupportCard> supportCards;
    public List<PowerupCard> powerupCards;
    public List<SpellCard> spellCards;

    public Card GetData(CardType cardType, PoolType type)
    {
        switch (cardType)
        {
            case CardType.Digits:
                return (Card)digitCards.Single(q => q.poolType == type);
            case CardType.Animals:
                return (Card)animalCards.Single(q => q.poolType == type);
            case CardType.Weapons:
                return (Card)weaponCards.Single(q => q.poolType == type);
            case CardType.Power_ups:
                //return (Card)animalCards.FirstOrDefault(q => q.poolType == type);
                return (Card)powerupCards.Single(q => q.poolType == type);
            case CardType.Spells:
                return (Card)spellCards.Single(q => q.poolType == type);
            case CardType.Support:
                return (Card)supportCards.Single(q => q.poolType == type);
            default:
                return null;
        }
    }
}

public enum SkinType
{
    Normal_Player = PoolType.Skin_Normal_Player,
    Normal_Bot = PoolType.Skin_Normal_Bot,
    Chicken = PoolType.Skin_Chicken,
    Bear = PoolType.Skin_Bear,
    Dino = PoolType.Skin_Dino,
    Dragon = PoolType.Skin_Dragon,
    Mammouth = PoolType.Skin_Mammouth,
    Scorpion = PoolType.Skin_Scorpion,
    Spider = PoolType.Skin_Spider,
    Wolf = PoolType.Skin_Wolf,
    Bot_Bow = PoolType.Skin_Bot_Bow
}

public enum CardType
{
    Power_ups,
    Animals,
    Spells,
    Weapons,
    Support,
    Digits
}

public enum BulletType
{
    Bullet_Cannon = PoolType.Bullet_Cannon,
    Bullet_CrossBow = PoolType.Bullet_Crossbow,
    Bullet_Bow = PoolType.Bullet_Bow,
    Bullet_Gun = PoolType.Bullet_Gun
}
