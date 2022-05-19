using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalBossMoving : BaseState
{
    private FinalBoss_FSM _fbsm;
    
    public FinalBossAI finalBossAI; //declares public BossAI script 


    public FInalBossMoving (FinalBoss_FSM stateMachine) : base("moving", stateMachine)
    {
        _fbsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _fbsm.finalBossAnim.Play("moving"); //play moving animation

        finalBossAI = _fbsm.finalBossAI;
        finalBossAI.InvokeRepeating("UpdatePath", 0, 0.1f); //begin updating the path

        _fbsm.fbm.shouldFlip = true;
        _fbsm.hitCondition = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (finalBossAI.attackPlayer == true) //if BossAI script detects target (player) within range
        {
           //_fbsm.ChangeState(_fbsm.weakattack); //transition to attack state;
        }

        if (_fbsm.hitCondition == true)
        {
            //_fbsm.ChangeState(_fbsm.hit);
        }

        if(_fbsm.finalBossDead)
        {
            //_fbsm.ChangeState(_fbsm.dead);
        }
    }

    public override void Exit()
    {
        base.Exit();
        finalBossAI.CancelInvoke("UpdatePath");
        finalBossAI.path = null;
    }
}
