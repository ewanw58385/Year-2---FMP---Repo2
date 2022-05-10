using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportState : BaseState
{
    public Enemy_FSM _EFSM;

    public Animator VFXAnim;
    
    public EnemyTeleportState(Enemy_FSM statemachine) : base("teleport", statemachine)
    {
        _EFSM = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        VFXAnim = GameObject.Find("EnemyVFX").GetComponent<Animator>();

        Vector2 teleportDirection = new Vector2(_EFSM.player.transform.position.x - _EFSM.transform.position.x, 0).normalized; //Vector2 variable to determine the direction the player is in
        _EFSM.transform.position = new Vector2(_EFSM.player.transform.position.x - teleportDirection.x * _EFSM.teleportDistance, _EFSM.rb.position.y); //move the enemy's position to a new vector: Player's position on the x - the direction to teleport, multiplied by the distance teleported.
        
        VFXAnim.Play("enemyTeleport"); //play teleport anim (enemy now at teleported location)
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

         if (VFXAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) //if teleport anim has finished 
        {
            VFXAnim.SetTrigger("finishedTeleporting"); //reset for next teleport anim
            _EFSM.ChangeState(_EFSM.idle); //change state
        }
    }
}
