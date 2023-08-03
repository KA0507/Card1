using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public void OnInit()
    {
        counter.Cancel();
        hp = 80;
        target = null;
        isAttack = false;
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(SkinType.Normal);
        currentSkin.renderer.material = LevelManager.Ins.materials[1];
    }
    
    public void OnInit(Character character)
    {
        counter.Cancel();
        hp = character.hp;
        target = null;
        isAttack = false;
        CancelInvoke();
        damage = character.damage;
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(character.currentSkin.skinType);

    }

    // Update is called once per frame
    void Update()
    {
        counter.Execute();
        if (isDead)
        {
            return;
        }

        if (isAttack)
        {
            return;
        }
        if (GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            /*if (LevelManager.Ins.currentLevel.bots.Count > 0*//* && moveTween == null && isAttack == false*//*)
            {
                Move();
            }*/
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
        if (LevelManager.Ins.currentLevel.bots.Count > 0)
        {
            int i = Random.Range(0, LevelManager.Ins.currentLevel.bots.Count);
            target = LevelManager.Ins.currentLevel.bots[i];
        }
    }
    public override void Move()
    {
        base.Move();
        //moveTween = TF.DOMove(target.TF.position, 3f);
        TF.position = Vector3.MoveTowards(TF.position, target.TF.position, Time.deltaTime * moveSpeed);
    }

    public override void StopMove()
    {
        //Debug.Log("stopmove");
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
        LevelManager.Ins.currentLevel.players.Remove(this);
        Invoke("Despawn", 2f);
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
            if (c is Bot)
            {
                Attack();
                moveTween?.Kill();
            }
        }*/
    }
}
