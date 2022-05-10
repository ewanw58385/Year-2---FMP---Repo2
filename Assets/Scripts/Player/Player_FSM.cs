using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FSM : G_FSM //this is the StateMachine class for MOVEMENT. It inherits from the StateMachine class (so contains all functionality of the generic state machine)
{
    [HideInInspector]
    public IdleState idle; //Declares the states for this machine to be initialised 
    [HideInInspector]
    public WalkingState walking;
    [HideInInspector]
    public SpawnState spawn;
    [HideInInspector]
    public JumpState jump;
    [HideInInspector]
    public RunState running;
    [HideInInspector]
    public WeakAttackState weakattack;

    [HideInInspector]
    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;

    public float moveSpeed = 4f;
    public float runSpeed = 6.75f;
    public float jumpForce = 8f;
    public float jumpMoveSpeed = 3f;

    public void Awake()
    {
        player = transform; //set the transform for states here 


        idle = new IdleState(this); //IdleState instance = new instance, passing "this" stateMachine as a parameter to the state's constructor 
                                    //(using overloading since the Base contstructor takes two parameters but only one is of type "StateMachine")

        walking = new WalkingState(this);
        spawn = new SpawnState(this);
        jump = new JumpState(this);
        running = new RunState(this);
        weakattack = new WeakAttackState(this);
    }

    protected override BaseState GetInitialState() //overrides BaseState GetInitialState function to return the correct state
    {
        return spawn; //first state to be used. Can be whatever state you want. 
    }

        public bool GroundCheck()
    {
        return player.GetComponent<PlayerManager>().isGrounded;
    }
}
