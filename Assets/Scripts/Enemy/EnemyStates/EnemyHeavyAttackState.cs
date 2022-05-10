using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyAttackState : BaseState
{
    private Enemy_FSM _EFSM;

    public EnemyHeavyAttackState(Enemy_FSM statemachine) : base("heavyattack", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        //_EFSM.rb.velocity = Vector2.zero;

        _EFSM.enemyAnim.Play("sweepattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.3f)
        {
            _EFSM.ChangeState(_EFSM.moving);
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
}
