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

    public Card GetData(CardType cardType, PoolType type)
    {
        switch (cardType)
        {
            case CardType.Digits:
                return (Card)digitCards.Single(q => q.poolType == type);
            case CardType.Animals:
                return (Card)animalCards.Single(q => q.poolType == type);
            default:
                return null;
        }
    }
}

public enum AccessoryType
{
    None,
}

public enum SkinType
{
    Normal = PoolType.Skin_Normal,
    Chicken = PoolType.Skin_Chicken,
    Bear = PoolType.Skin_Bear,
    Dino = PoolType.Skin_Dino,
    Dragon = PoolType.Skin_Dragon,
    Mammouth = PoolType.Skin_Mammouth,
    Scorpion = PoolType.Skin_Scorpion,
    Spider = PoolType.Skin_Spider,
    Wolf = PoolType.Skin_Wolf,
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
