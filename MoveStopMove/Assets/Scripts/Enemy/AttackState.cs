using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float coolDown;
    public override void Enter()
    {
        
    }

    public override void Exit()
    {

    }

    public override void Performed()
    {
        Attack();
        PatrolSwitch();
        DeadSwitch();
    }
    public void Attack()
    {
        if (enemy.coolDown == 0)
        {
            enemy.animator.SetBool("IsIdle", true);
            enemy.animator.SetBool("IsAttack", false);
        }
        else if (enemy.coolDown >= 5)
        {
            enemy.animator.SetBool("IsIdle", false);
            enemy.animator.SetBool("IsAttack", true);
        }
    }
    public void PatrolSwitch()
    {
        for (int i = 0; i < enemy.targetList.Count; i++)
        {
            if (enemy.targetList.Count <= 0)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public void DeadSwitch()
    {
        //Debug.Log(enemy.isDead);
        if (enemy.isDead)
        {
            stateMachine.ChangeState(new DeadState());
        }
    }
}
