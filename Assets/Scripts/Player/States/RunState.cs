using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    private Player_FSM _psm;

    private float horizontalInput;


    public RunState(Player_FSM stateMachine) : base("running", stateMachine)
    {
        _psm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _psm.anim.Play("Running");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Input.GetKeyDown(KeyCode.Space) && _psm.GroundCheck()) //if pressed space bar and on the ground
        {
            _psm.ChangeState(_psm.jump); //jump
        }
        
        if (!Input.GetKey(KeyCode.LeftShift)) //if not holding left shift key
        {
            _psm.ChangeState(_psm.walking); //transition back to walking
        }
    }

    public override void UpdatePhysics()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        _psm.rb.velocity = new Vector2((horizontalInput) * _psm.runSpeed, _psm.rb.velocity.y);
    }
}
