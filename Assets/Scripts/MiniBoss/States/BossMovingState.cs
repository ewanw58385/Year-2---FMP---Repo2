using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovingState : BaseState
{   
    private Boss_FSM _bfsm;

    public BossAI BossAI; //declares public BossAI script 

    public BossMovingState(Boss_FSM stateMachine) : base("moving", stateMachine)
    {
        _bfsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bfsm.bossAnim.Play("moving"); //play moving animation

        BossAI = _bfsm.bossAI;
        BossAI.InvokeRepeating("UpdatePath", 0, 0.1f); //begin updating the path

        _bfsm.bm.shouldFlip = true;
        //_bfsm.hitCondition = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();


        if (BossAI.playerWithinRange == false) //if BossAI script detects target (player) out of range
        {
           _bfsm.ChangeState(_bfsm.idle); //transition to idle state;
        }

        if (BossAI.attackPlayer == true) //if BossAI script detects target (player) within range
        {
           _bfsm.ChangeState(_bfsm.weakattack); //transition to attack state;
        }

        if (_bfsm.hitCondition == true)
        {
            _bfsm.ChangeState(_bfsm.hit);
        }

        /*
        if(_bfsm.enemyDead)
        {
            _bfsm.ChangeState(_bfsm.dead);
        } */
    }

    public override void Exit()
    {
        base.Exit();
        BossAI.CancelInvoke("UpdatePath");
        BossAI.path = null;
    }
}
