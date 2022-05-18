using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeadState : BaseState
{
    public Boss_FSM _bfsm;

    public BossDeadState(Boss_FSM statemachine) : base("dead", statemachine)
    {
        _bfsm = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _bfsm.bossAnim.Play("dead"); //play dead anim
        _bfsm.bm.shouldFlip = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                GameObject.Destroy(GameObject.Find("Boss Health Bar"));
            }
    }
}