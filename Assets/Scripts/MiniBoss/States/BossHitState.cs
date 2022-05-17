using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitState : BaseState
{
    private Boss_FSM _bfsm;
    private BossCombatManager _bcm;

    public float knockbackForceX = 2f; //4
    public float knockbackForceY = 10f; //20

    private bool knockbackOnce = true;

    public BossHitState(Boss_FSM statemachine) : base("hitstate", statemachine)
    {
        _bfsm = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _bfsm.hitCondition = false; //reset the condition to transition to this state
        _bfsm.bm.shouldFlip = true; //should be able to flip if hit

        _bcm = _bfsm.transform.GetComponent<BossCombatManager>(); //gets reference to combat manager for deducting health 
        _bfsm.player = GameObject.Find("Player"); //for applying a knockback effect in the right direction

        _bfsm.bossAnim.Play("hit"); //plays hit anim
        _bcm.ApplyDamage(_bfsm.damageTaken); //calls the apply damage function from the EnemyCombatManager, passing the damageTaken parameter (set in the player attack state) as the damage to be deducted
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

            if (knockbackOnce)
            {
                Vector2 knockbackDirection = new Vector2(_bfsm.player.transform.position.x - _bfsm.transform.position.x, _bfsm.player.transform.position.y - _bfsm.transform.position.y).normalized;
                Vector2 knockbackForce = new Vector2(knockbackDirection.x * knockbackForceX, knockbackDirection.y * knockbackForceY);

                _bfsm.bossRb.AddForce(-knockbackForce, ForceMode2D.Impulse);
                //Debug.Log(-knockbackForce);
            }
            
            knockbackOnce = false;

        if(_bfsm.bossDead)
        {
            _bfsm.ChangeState(_bfsm.dead);
        }

        if (_bfsm.hitCondition == true) //if gets hit again (while hit from first attack)
        {
            _bfsm.ChangeState(_bfsm.hit); //transition to hit (restart state)
        }

        if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f) //if hit finished
        {
            _bfsm.ChangeState(_bfsm.weakattack); //retaliate
        }
    }

    public override void Exit()
    {
        base.Exit();

        knockbackOnce = true;
        _bfsm.damageTaken = 0f; //reset the damage float 
    }

}
