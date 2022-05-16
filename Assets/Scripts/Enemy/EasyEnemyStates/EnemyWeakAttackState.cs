using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakAttackState : BaseState
{
    private Enemy_FSM _EFSM; 
    private EnemyCombatManager _ecm;

    private Animator _combatAnim;

    private bool preventAttackingEveryFrame;
    private float transitionDelay = 0.2f;

    public EnemyWeakAttackState(Enemy_FSM stateMachine) : base("weakattack", stateMachine)
    {
        _EFSM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _ecm = _EFSM.ecm; //instance of EnemyCombatManager
        _combatAnim = GameObject.Find("EnemyAttackVFX").GetComponent<Animator>();

        preventAttackingEveryFrame = true;
        _EFSM.em.shouldFlip = true; 
        _EFSM.hitCondition = false;

        _EFSM.enemyAnim.Play("weakattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f) //if attack animation is at the point of charging lightining 
        {
                _EFSM.em.shouldFlip = false; //prevent flipping while lightining animation is playing

                _combatAnim.Play("LightiningAnim"); //play lightining animation

            if (_combatAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f) //if lightining anim is at point of hitting player 
            {
                if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
                {
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_ecm.weakAttackPosition.position, _ecm.weakAttackRange, _ecm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                    foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                    {
                        _EFSM.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                        _EFSM.player.GetComponent<PlayerCombatManager>().TakeDamage(_ecm.weakAttackDamage); //apply damage to player passing weak attack damage as parameter
                        //Debug.Log(_ecm.weakAttackDamage);
                    }

                    preventAttackingEveryFrame = false; //prevent iterating again
                }
            }            
        }

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f + transitionDelay) //if anim finished
        {
            _EFSM.em.shouldFlip = true; //allow enemy to flip before entering heavy attack
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

        if (_EFSM.player != null) //if there is a player
        {
            float dist = Vector2.Distance(_EFSM.rb.position, _EFSM.player.transform.position); //calculate distance 

            if (dist >= _EFSM.enemyAI.aggroRange) //if player walks away while mid attack
            {
                _EFSM.ChangeState(_EFSM.moving); //transition to moving state
            }
        }
        else //if there is not a player
        {
            return; //return out of function
        }
    }

    public override void Exit()
    {
        base.Exit();

        _combatAnim.SetTrigger("lightiningStrike");

        preventAttackingEveryFrame = false;
    }
}
