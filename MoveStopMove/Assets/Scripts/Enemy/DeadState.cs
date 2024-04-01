using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public override void Enter()
    {
        enemy.DeadAnimDelay();
    }

    public override void Exit()
    {

    }

    public override void Performed()
    {
        
    }
}
