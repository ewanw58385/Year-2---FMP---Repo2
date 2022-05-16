using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakAttack : BaseState
{
    private Boss_FSM _bfsm; 
    private BossCombatManager _bm;

    private bool preventAttackingEveryFrame;

    public BossWeakAttack(Boss_FSM stateMachine) : base("weakattack", stateMachine)
    {
        _bfsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _bm = _bfsm.bcm; //instance of BossCombatManager

        preventAttackingEveryFrame = true;
        _bfsm.bm.shouldFlip = true; 
        //_bfsm.hitCondition = false;

        _bfsm.bossAnim.Play("weakattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) //if anim is at point of hitting player 
            {
                if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
                {
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_bm.weakAttackPosition.position, _bm.weakAttackRange, _bm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                    foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                    {
                        _bfsm.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                        _bfsm.player.GetComponent<PlayerCombatManager>().TakeDamage(_bm.weakAttackDamage); //apply damage to player passing weak attack damage as parameter
                    }

                    preventAttackingEveryFrame = false; //prevent iterating again
                }
            }            

        if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f) //if anim finished
        {
            _bfsm.bm.shouldFlip = true; //allow enemy to flip before entering heavy attack
           _bfsm.ChangeState(_bfsm.heavyattack); //transition to heavy attack state
        }

        if (_bfsm.hitCondition == true)
        {
            _bfsm.ChangeState(_bfsm.hit);
        }

        /*
        if(_bfsm.enemyDead)
        {
            _bfsm.ChangeState(_bfsm.dead);
        }*/

        if (_bfsm.player != null) //if there is a player
        {
            float dist = Vector2.Distance(_bfsm.bossRb.position, _bfsm.player.transform.position); //calculate distance 

            if (dist >= _bfsm.bossAI.aggroRange) //if player walks away while mid attack
            {
                _bfsm.ChangeState(_bfsm.moving); //transition to moving state
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
        preventAttackingEveryFrame = false;
    }
}
