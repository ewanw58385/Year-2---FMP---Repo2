using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakAttackState : BaseState
{
    private Player_FSM _psm;
    private PlayerCombatManager _pcm;

    private bool preventAttackingEveryFrame = true;
    
    public WeakAttackState(Player_FSM stateMachine) : base("weakattack", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    
        _pcm = _psm.GetComponent<PlayerCombatManager>();

        _psm.anim.Play("weak attack");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_psm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f) //if attack animation is at the point of hitting the enemy
        {
            if (preventAttackingEveryFrame) //code to apply damage in if statement to prevent the damage from being applied every frame
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_pcm.weakAttackPos.position, _pcm.weakAttackRange, _pcm.enemyLayer); //creates array of colliders which are within the boundary and are of the correct layermask 

                foreach (Collider2D enemyHit in enemiesToDamage) //for each enemy hit in the array declared above
                {
                    //enemyHit.GetComponent<EnemyCombatManager>().ApplyDamage(_pcm.weakAttackDamage); //apply damage
                    enemyHit.GetComponent<Enemy_FSM>().hitCondition = true; //set the condition for transitioning to hit state to true
                    enemyHit.GetComponent<Enemy_FSM>().damageTaken = _pcm.weakAttackDamage; //damage float for hit state

                    //Debug.Log("applied damage");
                }
                
                preventAttackingEveryFrame = false;
            }
        }

        if (_psm.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) //if attack animation has finished 
        {
            _psm.ChangeState(_psm.idle); //change back to idle
            preventAttackingEveryFrame = true; //allows damage to be taken next attack
        }
    }

    public override void UpdatePhysics()
    {

    }
}
