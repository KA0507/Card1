using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CardManager : Singleton<CardManager>
{
    public UIGamePlay uiGamePlay;  

    public void UsePowerupCard(List<Character> targets, Card card)
    {
        int n = targets.Count;
        PowerupCard pc = card as PowerupCard;

        switch (card.poolType)
        {
            case PoolType.Power_Accessory:
                for (int i = 0; i < targets.Count; i++)
                {
                    ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
                    targets[i].upProperties(pc.hp, pc.damage);
                    targets[i].currentSkin.ChanegeAccessory(targets[i]);
                }
                break;
            case PoolType.Power_UpSize:
                for (int i = 0; i < targets.Count; i++)
                {
                    ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
                    targets[i].upProperties(pc.hp, pc.damage);
                    targets[i].upSize(1.5f);
                }
                break;
            case PoolType.Power_Flash:
                for (int i = 0; i < targets.Count; i++)
                {
                    ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
                    targets[i].upProperties(pc.hp, pc.damage);
                }
                break;
            case PoolType.Power_UpHp:
                for (int i = 0; i < targets.Count; i++)
                {
                    ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
                    targets[i].upProperties(pc.hp, pc.damage);
                }
                break;
            case PoolType.Power_Water:
                for (int i = 0; i < targets.Count; i++)
                {
                    ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
                    targets[i].upProperties(pc.hp, pc.damage);
                }
                break;
        }
    }

    public void UseAnimalCard(List<Character> targets, Card card)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
            targets[i].ChangeSkin((SkinType)card.poolType);
        }
    }
    
    public void UseSpellCard(List<Character> targets, Card card)
    {
        int n = targets.Count;
        switch(card.poolType)
        {
            case PoolType.Spell_FireBall:
                break;
            case PoolType.Spell_UFO:
                break;
            case PoolType.Spell_Poison:
                for (int i = 0; i < n; i++)
                {
                    targets[i].Despawn();
                    LevelManager.Ins.currentLevel.bots.Remove(targets[i]);
                }
                break;
            case PoolType.Spell_ZoomOut:
                for (int i = 0; i < n; i++)
                {
                    targets[i].TF.DOScale(targets[i].TF.localScale.x / 2, 0.5f);
                    targets[i].hp /= 4;
                    targets[i].targetIndicator.SetHp(targets[i].hp);
                    targets[i].TF.DOMoveY(1f, 0.5f);
                }
                break;
            case PoolType.Spell_Balloon:
                break;
            case PoolType.Spell_Ally:
                break;
            case PoolType.Spell_Freeze:
                break;
            case PoolType.Spell_Sub50Percent:
                for (int i = 0; i < n; i++)
                {
                    targets[i].hp /= 2;
                    targets[i].targetIndicator.SetHp(targets[i].hp);
                }
                break;
        }
    }
    
    public void UseSupportCard(Card card)
    {
        if (card.poolType == PoolType.Support_Cannon || card.poolType == PoolType.Support_Crossbow)
        {
            Support support = SimplePool.Spawn<Support>(card.poolType, new Vector3(uiGamePlay.rangeCard.TF.position.x, 1.5f, uiGamePlay.rangeCard.TF.position.z), Quaternion.identity);
            support.OnInit();
            LevelManager.Ins.currentLevel.players.Add(support);
        }    
    }
    
    public void UseWeaponCard(List<Character> targets, Card card)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            ParticlePool.Play(ParticleType.Level_up, targets[i].TF.position, Quaternion.identity);
            targets[i].ChangeWeapon((WeaponType)card.poolType);
        }
    }
    
    public void UseDigitCard(List<Character> targets, Card card)
    {
        int n = targets.Count;

        switch (card.poolType)
        {
            case PoolType.Digit_Add1:
                if (targets.Count > 0)
                {
                    LevelManager.Ins.CreatePlayer();
                }
                break;
            case PoolType.Digit_Add3:
                if (targets.Count > 0)
                {
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                }
                break;
            case PoolType.Digit_Add5:
                if (targets.Count > 0)
                {
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                }
                break;
            case PoolType.Digit_Add7:
                if (targets.Count > 0)
                {
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                    LevelManager.Ins.CreatePlayer();
                }
                break;
            case PoolType.Digit_Mul2:
                for (int i = 0; i < targets.Count; i++)
                {
                    LevelManager.Ins.CreatePlayer(targets[i]);
                }
                break;
            case PoolType.Digit_Mul3:
                for (int i = 0; i < targets.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        LevelManager.Ins.CreatePlayer(targets[i]);
                    }
                }
                break;
            case PoolType.Digit_Mul4:
                for (int i = 0; i < targets.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        LevelManager.Ins.CreatePlayer(targets[i]);
                    }
                }
                break;
            case PoolType.Digit_Mul5:
                for (int i = 0; i < targets.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        LevelManager.Ins.CreatePlayer(targets[i]);
                    }
                }
                break;
            case PoolType.Digit_Sub3:
                for (int i = 0; i < targets.Count; i++)
                {
                    if (i == 3) break;
                    targets[i].Despawn();
                    LevelManager.Ins.currentLevel.bots.Remove(targets[i]);
                }
                break;
            case PoolType.Digit_Div2:
                for (int i = 0; i < n / 2; i++)
                {
                    targets[i].Despawn();
                    LevelManager.Ins.currentLevel.bots.Remove(targets[i]);
                }
                break;
            case PoolType.Digit_Div3:
                for (int i = 0; i < n * 2 / 3; i++)
                {
                    targets[i].Despawn();
                    LevelManager.Ins.currentLevel.bots.Remove(targets[i]);
                }
                break;
        }
        
    }

    public void UseCard(List<Character> targets, Card card)
    {
        switch (card.cardType)
        {
            case CardType.Power_ups:
                UsePowerupCard(targets, card);
                break;
            case CardType.Animals:
                UseAnimalCard(targets, card);
                break;
            case CardType.Spells:
                UseSpellCard(targets, card);
                break;
            case CardType.Support:
                UseSupportCard(card);
                break;
            case CardType.Weapons:
                UseWeaponCard(targets, card);
                break;
            case CardType.Digits:
                UseDigitCard(targets, card);
                break;
        }
    }
}
