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
    public virtual void FindTarget()
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

    public virtual void upProperties(int hp, int damage)
    {
        this.hp += hp;
        this.damage += damage;
        targetIndicator.SetHp(this.hp);
    }

    public virtual void upSize(float s)
    {
        TF.position += Vector3.up*TF.localScale.x*0.5f;
        TF.DOScale(new Vector3(s*TF.localScale.x, s*TF.localScale.y, s*TF.localScale.z), 0.5f);
    }

    public void ChangeWeapon(WeaponType weapon, bool upProperties = true)
    {
        currentSkin.ChanegeWeapon(this, weapon, upProperties);
        targetIndicator.SetHp(this.hp);
    }
    public void ChangeSkin(SkinType skinType)
    {
        if (currentSkin != null)
        {
            SimplePool.Despawn(currentSkin);
        }
        currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
        if (skinType != SkinType.Normal_Player && skinType != SkinType.Normal_Bot)
        {
            if (this is Player || skinType == SkinType.Chicken)
            {
                currentSkin.OnInit();
                hp = Data.Ins.skinData.GetDataSkin(skinType).hp;
                damage = Data.Ins.skinData.GetDataSkin(skinType).damage;
            }
        }
        else
        {
            currentSkin.OnInit();
        }
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
