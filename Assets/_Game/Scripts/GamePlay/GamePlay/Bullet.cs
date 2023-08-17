using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : GameUnit
{
    public Character character;
    public float speed;
    public Character target;
    public int damage;
    public virtual void OnInit(Character character, Character target, int damage)
    {
        this.character = character;
        this.target = target;
        this.damage = damage;
        TF.forward = (target.TF.position - TF.position).normalized;
        TF.DOJump(target.TF.position, 1, 1, 0.5f).OnComplete(OnDespawn);
    }

    // Despawn bullet
    public void OnDespawn()
    {
        target.hp -= damage;
        if (target.isDead)
        {
            target.targetIndicator.SetHp(0);
            target.Death();
            character.FindTarget();
        }
        else
        {
            target.targetIndicator.SetHp(target.hp);
        }
        character.isAttack = false;
        ParticlePool.Play(ParticleType.Hit, TF.position, Quaternion.identity);
        SimplePool.Despawn(this);
    }
}
