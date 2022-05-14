using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeadState : BaseState
{
    public Enemy_FSM _EFSM;
    public EnemyCombatManager _ECM;

    private HiddenTiles _ht;

    public EnemyDeadState(Enemy_FSM statemachine) : base("dead", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _ECM = _EFSM.GetComponent<EnemyCombatManager>();
        _ht = GameObject.Find("HiddenTiles").GetComponent<HiddenTiles>();
        _EFSM.enemyAnim.Play("deadAnim");

        _EFSM.em.shouldFlip = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                _ECM.DestroyGameObject();
                _ht.InstantiateItemPickup();
            }
    }
}
