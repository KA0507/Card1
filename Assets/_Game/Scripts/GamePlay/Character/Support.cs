using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Character
{
    public BulletType bulletType;
    public void OnInit()
    {
        counter.Cancel();
        TF.localScale = new Vector3(1, 1, 1);
        target = null;
        isAttack = false;
        CancelInvoke();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(this);
    }

    // Update is called once per frame
    void Update()
    {
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
                Attack();
            }
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
    public override void Attack()
    {
        base.Attack();
        //ChangeAnim(Constant.ANIM_ATTACK);
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
            /*if (target.isDead)
            {
                FindTarget();
                return;
            }*/
            Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, TF.position, Quaternion.identity);
            bullet.OnInit(this, target, damage);
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
