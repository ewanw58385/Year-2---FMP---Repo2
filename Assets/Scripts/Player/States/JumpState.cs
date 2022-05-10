using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JumpState : BaseState 
{
    private Player_FSM _psm;

    private float horizontalInput;
    private Vector2 jumpDirection; 

    private bool _isGrounded;

    public JumpState(Player_FSM stateMachine) : base("jump", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _psm.rb.gravityScale = 5f; //increases gravity for better jump
        _psm.rb.AddForce(Vector2.up * _psm.jumpForce, ForceMode2D.Impulse);

        _psm.anim.Play("Jump");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

            horizontalInput = Input.GetAxisRaw("Horizontal"); //gets axis as vector2
            _psm.rb.velocity = new Vector2(horizontalInput * _psm.jumpMoveSpeed, _psm.rb.velocity.y); //applies velocity on the X axis while in the air without affecting Y velocity from jump

         if(_psm.rb.velocity.y <= 0 && _psm.GroundCheck()) //if falling + touching ground
         {
            _psm.ChangeState(_psm.idle); //transition to idle
         }
    }

    public override void Exit()
    {
        base.Exit();

        _psm.rb.gravityScale = 3f; //resets gravity 
    
    }
}

