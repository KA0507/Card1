using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public void OnInit()
    {
        counter.Cancel();
        TF.localScale = new Vector3(1, 1, 1);
        hp = Data.Ins.levelPlayer.playerLevels[PlayerPrefs.GetInt(UserData.KEY_LEVEL_PLAYER)].hp;
        damage = Data.Ins.levelPlayer.playerLevels[PlayerPrefs.GetInt(UserData.KEY_LEVEL_PLAYER)].damage;
        target = null;
        isAttack = false;
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(SkinType.Normal_Player);
    }

    public void OnInit(SkinType skin)
    {
        counter.Cancel();
        TF.localScale = new Vector3(1, 1, 1);
        hp = Data.Ins.levelPlayer.playerLevels[PlayerPrefs.GetInt(UserData.KEY_LEVEL_PLAYER)].hp;
        damage = Data.Ins.levelPlayer.playerLevels[PlayerPrefs.GetInt(UserData.KEY_LEVEL_PLAYER)].damage;
        target = null;
        isAttack = false;
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(skin);
        //currentSkin.renderer.material = LevelManager.Ins.materials[1];
    }

    public void OnInit(Character character)
    {
        counter.Cancel();
        TF.localScale = character.TF.localScale;
        hp = character.hp;
        target = null;
        isAttack = false;
        CancelInvoke();
        damage = character.damage;
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
        ChangeSkin(character.currentSkin.skinType);
        if (character.currentSkin.currentWeapon != null)
        {
            ChangeWeapon(character.currentSkin.currentWeapon.weaponType, false);
        }
        if (character.currentSkin.currentAccessory != null)
        {
            currentSkin.ChanegeAccessory(this, false);
        }
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
        TF.position = Vector3.MoveTowards(TF.position, new Vector3(target.TF.position.x, TF.position.y, target.TF.position.z), Time.deltaTime * moveSpeed);
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

        // Tính toán góc xoay quanh trục Y dựa trên mục tiêu và vị trí hiện tại
        Vector3 directionToTarget = target.TF.position - TF.position;
        directionToTarget.y = 0; // Đặt y thành 0 để không làm thay đổi trục Y
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Sử dụng DOTween để thực hiện xoay quanh trục Y
        transform.DORotate(new Vector3(0, targetRotation.eulerAngles.y, 0), 0.5f);

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
        LevelManager.Ins.currentLevel.players.Remove(this);
        Invoke("Despawn", 2f);
    }

    public override void Despawn()
    {
        SimplePool.Despawn(this);
        SimplePool.Despawn(targetIndicator);
    }
}
