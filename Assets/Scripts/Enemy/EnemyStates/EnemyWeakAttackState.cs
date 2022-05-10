using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakAttackState : BaseState
{
    private Enemy_FSM _EFSM;

    public EnemyWeakAttackState(Enemy_FSM stateMachine) : base("weakattack", stateMachine)
    {
        _EFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        //_EFSM.rb.velocity = Vector2.zero; //disable velocity while player is attacking

        _EFSM.enemyAnim.Play("weakattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) //if anim finished
        {
            _EFSM.ChangeState(_EFSM.heavyattack); //transition to moving state 
        }

        if (_EFSM.hitCondition == true)
        {
            _EFSM.ChangeState(_EFSM.hitstate);
        }

        if(_EFSM.enemyDead)
        {
            _EFSM.ChangeState(_EFSM.dead);
        }

        float dist = Vector2.Distance(_EFSM.rb.position, _EFSM.player.transform.position);
        if (dist >= _EFSM.enemyAI.aggroRange) //if player walks away while mid attack
        {
            _EFSM.ChangeState(_EFSM.moving); //transition to moving state
        }

    }
}
