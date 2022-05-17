using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakAttack : BaseState
{
    private Boss_FSM _bfsm; 
    private BossCombatManager _bcm;

    private bool preventAttackingEveryFrame;
    private float transitionDelay = 0.5f;

    public BossWeakAttack(Boss_FSM stateMachine) : base("weakattack", stateMachine)
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

        _bfsm.bossRb.velocity = Vector2.zero;

        _bfsm.bossAnim.Play("weakattack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

            if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) //if anim is at point of hitting player 
            {
                if (preventAttackingEveryFrame) //apply damage in if statement to prevent the damage from being applied every frame
                {
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(_bcm.weakAttackPosition.position, _bcm.weakAttackRange, _bcm.playerLayerMask); //creates array of colliders which are within the boundary and are of the correct layermask 

                    foreach (Collider2D playerHit in playerToDamage) //for each enemy hit in the array declared above
                    {
                        _bfsm.player.GetComponent<Player_FSM>().hasBeenHit = true; //set the condition for transitioning to hit state to true
                        _bfsm.player.GetComponent<PlayerCombatManager>().TakeDamage(_bcm.weakAttackDamage); //apply damage to player passing weak attack damage as parameter
                    }

                    preventAttackingEveryFrame = false; //prevent iterating again
                }
            }            

        if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f + transitionDelay) //if anim finished
        {
            _bfsm.bm.shouldFlip = true; //allow enemy to flip before entering heavy attack
           _bfsm.ChangeState(_bfsm.heavyattack); //transition to heavy attack state
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

    public override void Exit()
    {
        base.Exit();
        preventAttackingEveryFrame = false;
    }
}
