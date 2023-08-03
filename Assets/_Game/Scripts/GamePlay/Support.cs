using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                if (Vector3.Distance(target.TF.position, TF.position) < 2f)
                {
                    Attack();
                }
            }
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
    public override void Attack()
    {
        base.Attack();
        ChangeAnim(Constant.ANIM_ATTACK);
        isAttack = true;
        counter.Start(() =>
        {
            if (target.isDead == true)
            {
                target.Death();
                //target = null;
                FindTarget();
            }
            target.hp -= damage;
            if (target.hp >= 0)
            {
                target.targetIndicator.SetHp(target.hp);
            }
            else
            {
                target.targetIndicator.SetHp(0);
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
}
