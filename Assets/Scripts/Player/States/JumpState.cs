using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JumpState : BaseState 
{
    private Player_FSM _psm;

    private float horizontalInput;
    private Vector2 jumpDirection; 

    private bool _isGrounded;
    private bool secondJump = true; //for preventing multiple jumps in air

    public JumpState(Player_FSM stateMachine) : base("jump", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _psm.rb.AddForce(Vector2.up * _psm.jumpForce, ForceMode2D.Impulse);

        _psm.anim.Play("Jump");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_psm.hasBeenHit)
        {
            _psm.ChangeState(_psm.hit);
        }

        if(_psm.doubleJumpUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Space) && secondJump) //if pressed space bar and is only the second jump
            {
                _psm.rb.velocity = Vector2.zero;
                _psm.rb.AddForce(Vector2.down * _psm.doubleJumpCounterForce, ForceMode2D.Impulse); //downwards force to slowdown the increase in velocity
                _psm.rb.AddForce(Vector2.up * _psm.doubleJumpForce, ForceMode2D.Impulse); //jump again
                _psm.anim.Play("Jump");
                secondJump = false; //prevent jumping again
            }
        }
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
        secondJump = true;
    }
}

