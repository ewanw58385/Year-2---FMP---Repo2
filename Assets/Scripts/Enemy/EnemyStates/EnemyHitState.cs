using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : BaseState
{
    private Enemy_FSM _EFSM;
    private EnemyCombatManager _ECM;

    public float knockbackForceX = 150;
    public float knockbackForceY = 1250;

    public EnemyHitState(Enemy_FSM statemachine) : base("hitstate", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _ECM = _EFSM.transform.GetComponent<EnemyCombatManager>(); //gets reference to combat manager for deducting health 

        _EFSM.player = GameObject.Find("Player"); //for applying a knockback effect in the right direction
        
        _EFSM.enemyAnim.Play("hitAnim"); //plays hit anim
        _ECM.ApplyDamage(_EFSM.damageTaken); //calls the apply damage function from the EnemyCombatManager, passing the damageTaken parameter (set in the player attack state) as the damage to be deducted
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(_EFSM.enemyDead)
        {
            _EFSM.ChangeState(_EFSM.dead);
        }
        
        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f) //if hit animation has finished 
        {
            float randomNumber = Random.Range(1, 5); //returns a value between 1 and 4 (1/4 chance of teleporting if hit)
            //float randomNumber = 1;

            if (randomNumber == 1) //teleport
            {
                _EFSM.ChangeState(_EFSM.teleport);

            }
            else //knockback 
            {
                Vector2 knockbackDirection = new Vector2(_EFSM.player.transform.position.x - _EFSM.transform.position.x, _EFSM.player.transform.position.y - _EFSM.transform.position.y).normalized;
                Vector2 knockbackForce = new Vector2(knockbackDirection.x * knockbackForceX, knockbackDirection.y * knockbackForceY);

                _EFSM.rb.AddForce(-knockbackForce);
                _EFSM.ChangeState(_EFSM.weakattack);

                Debug.Log(knockbackForce);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        _EFSM.damageTaken = 0f; //reset the damage float 
        _EFSM.hitCondition = false; //reset the condition to transition to this state
    }

}
