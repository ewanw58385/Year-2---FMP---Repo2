using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    private Enemy_FSM _EFSM; //declares private enemy state-machine

    public EnemyAI enemyAI; //declares public enemyAI script 

     public EnemyIdle(Enemy_FSM stateMachine) : base("idle", stateMachine)
    {
        _EFSM = stateMachine;
    }

     public override void Enter()
    {
        base.Enter();

        enemyAI = _EFSM.enemyAI; //sets reference of enemyAI script

        _EFSM.enemyAnim.Play("Idle"); //plays idle animation
        _EFSM.rb.velocity = Vector2.zero; //stop AI from moving once player is out of range
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.hitCondition == true)
        {
            _EFSM.ChangeState(_EFSM.hitstate);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (enemyAI.playerWithinRange == true) //if enemyAI script detects target (player) within range
        {
           _EFSM.ChangeState(_EFSM.moving); //transition to moving state;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
