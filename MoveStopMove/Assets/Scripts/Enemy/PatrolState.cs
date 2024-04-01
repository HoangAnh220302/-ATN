using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    public float radius = 10f;
    public float wanderRadius = 8f;
    public float wanderTimer = 5f;
    private float timer;
    public override void Enter()
    {
        timer = wanderTimer;
    }
    public override void Performed()
    {
        MoveToRandomPosition();
        AttackSwitch();
        DeadSwitch();
    }
    public override void Exit()
    {

    }
    void MoveToRandomPosition()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += enemy.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            enemy.Agent.SetDestination(hit.position);
            timer = 0;
            enemy.animator.SetBool("IsRun",true);
        }
        enemy.animator.SetBool("IsIdle", true);
    }
    public void AttackSwitch()
    {
        for (int i = 0; i < enemy.targetList.Count; i++)
        {
            if (enemy.coolDown >= 5 && enemy.targetList.Count >= 1)
            {
                stateMachine.ChangeState(new AttackState());
            }
        }
    }
    public void DeadSwitch()
    {
        if (enemy.isDead)
        {
            stateMachine.ChangeState(new DeadState());
        }
    }
}

