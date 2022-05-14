using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakAttackState : BaseState
{
    private Enemy_FSM _EFSM; 
    private EnemyCombatManager _ecm;

    private bool preventAttackingEveryFrame;

    public EnemyWeakAttackState(Enemy_FSM stateMachine) : base("weakattack", stateMachine)
    {
        _EFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _ecm = _EFSM.ecm; //instance of EnemyCombatManager

        preventAttackingEveryFrame = true;

        _EFSM.enemyAnim.Play("weakattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f) //if attack animation is at the point of hitting the player
        {
            if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
            {
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_ecm.weakAttackPosition.position, _ecm.weakAttackRange, _ecm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                {
                    _EFSM.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                    _EFSM.player.GetComponent<PlayerCombatManager>().TakeDamage(_ecm.weakAttackDamage); //apply damage to player passing weak attack damage as parameter
                    _EFSM.player.GetComponent<PlayerCombatManager>().KnockbackPlayer(_EFSM.transform.position);
                    //Debug.Log(_ecm.weakAttackDamage);
                }

                preventAttackingEveryFrame = false;
            }
        }

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) //if anim finished
        {
            _EFSM.ChangeState(_EFSM.heavyattack); //transition to moving state
            //preventAttackingEveryFrame = true;
 
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

    public override void Exit()
    {
        base.Exit();

        preventAttackingEveryFrame = false;
    }
}
