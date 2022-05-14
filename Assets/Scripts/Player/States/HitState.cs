using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BaseState
{
    private Player_FSM _psm;

    private PlayerCombatManager _pcm;

    public HitState(Player_FSM stateMachine) : base("hit", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _pcm = _psm.GetComponent<PlayerCombatManager>();

        _psm.anim.Play("hitAnim"); //play the hit animation
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    
        if (_psm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) //if hit animation has finished playing
        {
            if (_pcm.playerHealth <= 0) //if no health left
            {
                _psm.ChangeState(_psm.dead); //change to dead state
            }
            else
            {
                //_pcm.KnockbackPlayer();
                _psm.ChangeState(_psm.idle); //otherwise return to idle state
            }
        }
    }

   

    public override void Exit()
    {
        base.Exit();

        _psm.hasBeenHit = false; //reset hit bool for next attack
    }
}
