using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossIdle : BaseState
{
    private FinalBoss_FSM _fbsm;

    public FinalBossIdle (FinalBoss_FSM stateMachine) : base("idle", stateMachine)
    {
        _fbsm = stateMachine;
    }

     public override void Enter()
    {
        base.Enter();

        _fbsm.finalBossAnim.Play("idle"); //plays idle animation
        _fbsm.finalBossRb.velocity = Vector2.zero; //stop AI from moving once player is out of range

        _fbsm.fbm.shouldFlip = true;
        _fbsm.hitCondition = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_fbsm.hitCondition == true)
        {
            //_fbsm.ChangeState(_fbsm.hit);
        }

        if(_fbsm.finalBossDead)
        {
            //_fbsm.ChangeState(_fbsm.dead);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (_fbsm.finalBossAI.playerWithinRange == true) //if enemyAI script detects target (player) within range
        {
           _fbsm.ChangeState(_fbsm.moving); //transition to moving state;
        }
    }
}
