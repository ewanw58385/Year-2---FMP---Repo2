using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyAttackState : BaseState
{
    private Enemy_FSM _EFSM;
    private EnemyCombatManager _ecm;

    private bool preventAttackingEveryFrame;
    private float delayTransition = 0.25f;

    public EnemyHeavyAttackState(Enemy_FSM statemachine) : base("heavyattack", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        //_EFSM.rb.velocity = Vector2.zero;

        _ecm = _EFSM.ecm; //instance of EnemyCombatManager
        
        _EFSM.em.shouldFlip = false;
        preventAttackingEveryFrame = true;
        _EFSM.hitCondition = false;

        _EFSM.enemyAnim.Play("sweepattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

          if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f) //if attack animation is at the point of hitting the player
        {
            if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
            {
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_ecm.heavyAttackPosition.position, _ecm.heavyAttackRange, _ecm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                {
                    _EFSM.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                    _EFSM.player.GetComponent<PlayerCombatManager>().TakeDamage(_ecm.heavyAttackDamage); //apply damage to player passing weak attack damage as parameter
                }

                preventAttackingEveryFrame = false;
            }
        }

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.3f + delayTransition)
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

    public override void Exit()
    {
        base.Exit();
        preventAttackingEveryFrame = false;
    }
}
