using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public void OnInit()
    {
        counter.Cancel();
        hp = 60;
        target = null;
        isAttack = false;
        //currentAnimName = "";
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(SkinType.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        counter.Execute();

        if (isAttack)
        {
            return;
        }

        if (GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            if (target == null)
            {
                FindTarget();
            }
            else
            {
                if (Vector3.Distance(target.TF.position, TF.position) < 3f)
                {
                    StopMove();
                    Attack();
                }
                else
                {
                    Move();
                }
            }
        }
        if (GameManager.Ins.IsState(GameState.FINISH))
        {
            ChangeAnim(Constant.ANIM_WIN);
        }
    }

    public void FindTarget()
    {
        if (LevelManager.Ins.currentLevel.players.Count > 0)
        {
            int i = Random.Range(0, LevelManager.Ins.currentLevel.players.Count);
            target = LevelManager.Ins.currentLevel.players[i];
        } else
        {
            target = null;
        }
    }

    public override void Move()
    {
        base.Move();
        //moveTween = TF.DOMove(target.TF.position, 3f);
        TF.position =  Vector3.MoveTowards(TF.position, target.TF.position, Time.deltaTime * moveSpeed);
    }

    public override void StopMove()
    {
        /*moveTween?.Kill();
        moveTween = null;*/
    }
    public override void Attack()
    {
        base.Attack();
        ChangeAnim(Constant.ANIM_ATTACK);
        isAttack = true;
        counter.Start(() =>
        {
            target.hp -= damage;
            ParticlePool.Play(ParticleType.Hit, target.TF.position, Quaternion.identity);
            if (target.isDead == true)
            {
                target.targetIndicator.SetHp(0);
                target.Death();
                FindTarget();
            }
            else
            {
                target.targetIndicator.SetHp(target.hp);
            }
            ResetAttack();
        }, 1f);
    }

    public override void Death()
    {
        base.Death();
        LevelManager.Ins.currentLevel.bots.Remove(this);
        Invoke("Despawn", 1f);
    }

    public override void Despawn()
    {
        SimplePool.Despawn(this);
        SimplePool.Despawn(targetIndicator);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character c = Cache.GetCharacter(other);
            if (c is Player)
            {
                Attack();
            }
        }*/
    }
}
