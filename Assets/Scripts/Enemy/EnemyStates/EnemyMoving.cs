using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : BaseState
{   
    private Enemy_FSM _EFSM;

    public EnemyAI enemyAI; //declares public enemyAI script 

    public EnemyMoving(Enemy_FSM stateMachine) : base("moving", stateMachine)
    {
        _EFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _EFSM.enemyAnim.Play("moving"); //play moving animation

        enemyAI = _EFSM.enemyAI;
        enemyAI.InvokeRepeating("UpdatePath", 0, 0.1f); //begin updating the path

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (enemyAI.playerWithinRange == false) //if enemyAI script detects target (player) out of range
        {
           _EFSM.ChangeState(_EFSM.idle); //transition to idle state;
        }

        if (enemyAI.attackPlayer == true) //if enemyAI script detects target (player) out of range
        {
           _EFSM.ChangeState(_EFSM.weakattack); //transition to idle state;
        }

        if (_EFSM.hitCondition == true)
        {
            _EFSM.ChangeState(_EFSM.hitstate);
        }

        if(_EFSM.enemyDead)
        {
            _EFSM.ChangeState(_EFSM.dead);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        enemyAI.CancelInvoke("UpdatePath");
        enemyAI.path = null;
    }
}
