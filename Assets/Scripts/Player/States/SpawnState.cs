using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : BaseState
{
    private Player_FSM _psm;

    public SpawnState(Player_FSM stateMachine) : base("spawn", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _psm.anim.Play("SpawnIn"); //play spawn anim
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _psm.rb.velocity = Vector2.zero; //freeze movement while player is spawning

        if (_psm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f) //if spawn animation has finished playing
        {
            _psm.anim.SetBool("Spawned", true); //transition to idle anim
            _psm.ChangeState(_psm.idle); //change to idle state
            Debug.Log("Spawned");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
