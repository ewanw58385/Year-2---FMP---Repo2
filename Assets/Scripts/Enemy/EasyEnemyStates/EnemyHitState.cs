using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : BaseState
{
    private Enemy_FSM _EFSM;
    private EnemyCombatManager _ECM;

    public float knockbackForceX = 2f; //4
    public float knockbackForceY = 10f; //20

    private float randomNumber;
    private bool knockbackOnce = true;

    private Vector2 teleportResult;

    public EnemyHitState(Enemy_FSM statemachine) : base("hitstate", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        _EFSM.hitCondition = false; //reset the condition to transition to this state
        _EFSM.em.shouldFlip = true; //should be able to flip if hit

        _ECM = _EFSM.transform.GetComponent<EnemyCombatManager>(); //gets reference to combat manager for deducting health 
        _EFSM.player = GameObject.Find("Player"); //for applying a knockback effect in the right direction

        randomNumber = Random.Range(1, 5); //returns a value between 1 and 4 (to determine if should teleport or knockback)
        //randomNumber = 1;

        _EFSM.enemyAnim.Play("hitAnim"); //plays hit anim
        _ECM.ApplyDamage(_EFSM.damageTaken); //calls the apply damage function from the EnemyCombatManager, passing the damageTaken parameter (set in the player attack state) as the damage to be deducted

        Vector2 teleportDirection = new Vector2(_EFSM.player.transform.position.x - _EFSM.transform.position.x, 0).normalized; //Vector2 variable to determine the direction the player is in
        teleportResult = new Vector2(_EFSM.player.transform.position.x - teleportDirection.x * _EFSM.teleportDistance, _EFSM.rb.position.y); //Vector2 holding the position if enemy teleports: Player's position on the x - the direction to teleport, multiplied by the distance teleported.
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!Physics2D.Linecast(_EFSM.transform.position, teleportResult, _EFSM.walls)) //if there is not a "walls" collider within the current position and the teleport position (to prevent teleporting out of bounds)
        {
            if (randomNumber == 1)  
            {
                _EFSM.ChangeState(_EFSM.teleport); //teleport
            }
        } 

        else //knockback 
        {
            if (knockbackOnce)
            {
                Vector2 knockbackDirection = new Vector2(_EFSM.player.transform.position.x - _EFSM.transform.position.x, _EFSM.player.transform.position.y - _EFSM.transform.position.y).normalized;
                Vector2 knockbackForce = new Vector2(knockbackDirection.x * knockbackForceX, knockbackDirection.y * knockbackForceY);

                _EFSM.rb.AddForce(-knockbackForce, ForceMode2D.Impulse);
                //Debug.Log(-knockbackForce);
            }
            
            knockbackOnce = false;
        }

        if(_EFSM.enemyDead)
        {
            _EFSM.ChangeState(_EFSM.dead);
        }

        if (_EFSM.hitCondition == true) //if gets hit again (while hit from first attack)
        {
            _EFSM.ChangeState(_EFSM.hitstate); //transition to hit (restart state)
        }

        if (_EFSM.enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f) //delay before transitioning to attacking
        {
            _EFSM.ChangeState(_EFSM.weakattack);
        }
    }

    public override void Exit()
    {
        base.Exit();

        knockbackOnce = true;
        _EFSM.damageTaken = 0f; //reset the damage float 
    }

}
