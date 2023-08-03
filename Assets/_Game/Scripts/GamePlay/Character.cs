using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public Skin currentSkin;
    public Character target;
    public int hp;
    public int damage;
    public float moveSpeed;
    public bool isDead => hp <= 0;
    public bool isAttack = false;
    public Rigidbody rb;
    public Transform indicatorPoint;
    public TargetIndicator targetIndicator;
    public CounterTime counter = new CounterTime();

    public string currentAnimName;
    public Tween moveTween;

    public virtual void Move()
    {
        ChangeAnim(Constant.ANIM_RUN);
    }

    public virtual void StopMove()
    {

    }
    public virtual void Attack()
    {
        
    }

    public virtual void ResetAttack()
    {
        isAttack = false;
    }

    public virtual void Death()
    {
        ChangeAnim(Constant.ANIM_DIE);
    }

    public virtual void Despawn()
    {
        
    }
    public void ChangeWeapon(WeaponType weapon)
    {
        this.hp -= currentSkin.hp;
        this.damage -= currentSkin.damage;
        currentSkin.ChanegeWeapon(weapon);
        this.hp += currentSkin.hp;
        this.damage += currentSkin.damage;
    }
    public void ChangeSkin(SkinType skinType)
    {
        if (currentSkin != null)
        {
            SimplePool.Despawn(currentSkin);
        }
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
        hp += currentSkin.hp;
        damage += currentSkin.damage;
        targetIndicator.SetHp(hp);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName && currentSkin != null)
        {
            if (currentAnimName != null)
            {
                currentSkin.anim.ResetTrigger(currentAnimName);
            }
            currentAnimName = animName;
            currentSkin.anim.SetTrigger(currentAnimName);
        }
        if (currentAnimName == animName && animName == Constant.ANIM_ATTACK)
        {
            currentSkin.anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            currentSkin.anim.SetTrigger(currentAnimName);
        }
    }
}
