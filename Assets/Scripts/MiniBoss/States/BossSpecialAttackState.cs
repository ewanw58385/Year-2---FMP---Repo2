using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttackState : BaseState
{
    public Boss_FSM _bfsm;
    private BossCombatManager _bcm;

    private bool preventAttackingEveryFrame = true;
    private bool preventSecondAttackEveryFrame = true;

    private float transitionDelay = 0f;

    public BossSpecialAttackState(Boss_FSM stateMachine) : base("specialattack", stateMachine)
    {
        _bfsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _bcm = _bfsm.bcm; //instance of BossCombatManager

        preventAttackingEveryFrame = true;
        _bfsm.bm.shouldFlip = false; 
        _bfsm.hitCondition = false;

        _bfsm.bossAnim.Play("specialattack");
    }

     public override void UpdateLogic()
    {
        base.UpdateLogic();

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) //if anim is at point of hitting player 
            {
                if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
                {
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_bcm.specialAttackPosition.position, _bcm.specialAttackRange, _bcm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                    foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                    {
                        _bfsm.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                        _bfsm.player.GetComponent<PlayerCombatManager>().TakeDamage(_bcm.specialAttackDamage); //apply damage to player passing special attack damage as parameter
                    }

                    preventAttackingEveryFrame = false; //prevent iterating again
                }
            }  

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0.4f) //if enemy is between the first and second attack, check if the player has moved away
            {

                float dist = Vector2.Distance(_bfsm.bossRb.position, _bfsm.player.transform.position); //calculate distance 

                if (dist >= _bfsm.bossAI.withinContinuedAttackRange) //if player walks away while mid attack
                {
                    _bfsm.ChangeState(_bfsm.moving); //transition to moving state
                }
                else //if there is not a player
                {
                    return; //return out of function
                }
            }

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) //if anim is at point of hitting player a second time
            {
                if (preventSecondAttackEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
                {
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_bcm.specialAttackPosition.position, _bcm.specialAttackRange, _bcm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                    foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                    {
                        _bfsm.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                        _bfsm.player.GetComponent<PlayerCombatManager>().TakeDamage(_bcm.specialAttackDamage); //apply damage to player passing special attack damage as parameter
                        Debug.Log("secondslash");
                    }

                    preventSecondAttackEveryFrame = false; //prevent iterating again
                }
            }   

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f + transitionDelay) //if anim finished
            {
                _bfsm.bm.shouldFlip = true; //allow enemy to flip before entering heavy attack
                _bfsm.ChangeState(_bfsm.moving); //transition to heavy attack state

                preventAttackingEveryFrame = true;
                preventSecondAttackEveryFrame = true;
            }

            if (_bfsm.hitCondition == true)
            {
                _bfsm.ChangeState(_bfsm.hit);
            }

            if(_bfsm.bossDead)
            {
                _bfsm.ChangeState(_bfsm.dead);
            }
        }
}    

