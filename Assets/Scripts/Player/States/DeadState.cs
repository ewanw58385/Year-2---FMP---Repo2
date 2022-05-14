using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    private Player_FSM _psm;
    private PlayerCombatManager _pcm;

    public DeadState(Player_FSM stateMachine) : base("dead", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _pcm = _psm.GetComponent<PlayerCombatManager>();

        _psm.anim.Play("deadAnim"); //play dead animation
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _psm.rb.gravityScale = 15f;

        if (_psm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.1f) //if dead animation has finished 
        {
            _pcm.DestroyGameObject(); //destroy player
        }
    }
}
