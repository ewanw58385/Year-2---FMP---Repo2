using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : BaseState
{
    public Enemy_FSM _EFSM;
    public EnemyCombatManager _ECM;

    public EnemyDeadState(Enemy_FSM statemachine) : base("dead", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _ECM = _EFSM.GetComponent<EnemyCombatManager>();
        _EFSM.enemyAnim.Play("deadAnim");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                _ECM.DestroyGameObject();
            }
    }
}
