using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public void OnInit(int n)
    {
        counter.Cancel();
        TF.localScale = new Vector3(1, 1, 1);
        //hp = 60 + LevelManager.Ins.indexLevel*5;
        hp = Data.Ins.levelData.levelBots[LevelManager.Ins.indexLevel].bots[n].hp;
        damage = Data.Ins.levelData.levelBots[LevelManager.Ins.indexLevel].bots[n].damage;
        target = null;
        isAttack = false;
        //currentAnimName = "";
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(Data.Ins.levelData.levelBots[LevelManager.Ins.indexLevel].bots[n].skin);
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
            if (currentSkin.currentWeapon != null)
            {
                if (currentSkin.currentWeapon.poolType == PoolType.Weapon_Bow || currentSkin.currentWeapon.poolType == PoolType.Weapon_Gun)
                {
                    if (target == null)
                    {
                        FindTarget();
                    }
                    else
                    {
                        Throw();
                    }
                }
                else
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
            }
            else
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
        }
        if (GameManager.Ins.IsState(GameState.FINISH))
        {
            ChangeAnim(Constant.ANIM_WIN);
        }
    }

    public override void FindTarget()
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
        TF.position =  Vector3.MoveTowards(TF.position, new Vector3(target.TF.position.x, TF.position.y, target.TF.position.z), Time.deltaTime * moveSpeed);
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

    public void Throw()
    {
        ChangeAnim(Constant.ANIM_ATTACK);
        isAttack = true;
        //TF.DORotate(target.TF.position - TF.position, 0.5f);

        // Tính toán góc xoay quanh trục Y dựa trên mục tiêu và vị trí hiện tại
        Vector3 directionToTarget = target.TF.position - TF.position;
        directionToTarget.y = 0; // Đặt y thành 0 để không làm thay đổi trục Y
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Sử dụng DOTween để thực hiện xoay quanh trục Y
        transform.DORotate(new Vector3(0, targetRotation.eulerAngles.y, 0), 0.5f);

        counter.Start(() =>
        {
            if (currentSkin.currentWeapon.poolType == PoolType.Weapon_Bow)
            {
                Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Bow, TF.position, Quaternion.identity);
                bullet.OnInit(this, target, damage);
            }
            else
            {
                Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Gun, TF.position, Quaternion.identity);
                bullet.OnInit(this, target, damage);
            }
            Invoke("ResetAttack", 0.5f);
        }, 0.5f);
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
}
