using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FSM : G_FSM
{
    [HideInInspector] public EnemyIdle idle;
    [HideInInspector] public EnemyMoving moving;
    [HideInInspector] public EnemyWeakAttackState weakattack;
    [HideInInspector] public EnemyHeavyAttackState heavyattack;
    [HideInInspector] public EnemyHitState hitstate;
    [HideInInspector] public EnemyTeleportState teleport;
    [HideInInspector] public EnemyDeadState dead;


    [HideInInspector] public Animator enemyAnim;
    [HideInInspector] public EnemyAI enemyAI;
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public GameObject player;

    [HideInInspector] public bool hitCondition; //condition for transitioning to hit state, set to true on Player attack state 
    [HideInInspector] public float damageTaken; //float for holding the amount of damage taken. declared on player attack state, passed as a parameter within enemy Hit state
    [HideInInspector] public bool enemyDead; //bool for transitioning to dead state. Determined in the enemy combat manager. 
    public float teleportDistance = 4f;

    public void Awake()
    {
        idle = new EnemyIdle(this);
        moving = new EnemyMoving(this);
        weakattack = new EnemyWeakAttackState(this);
        heavyattack = new EnemyHeavyAttackState(this);
        hitstate = new EnemyHitState(this);
        teleport = new EnemyTeleportState(this);
        dead = new EnemyDeadState(this);

        enemyAI = GetComponent<EnemyAI>(); //gets reference of the AI script for states to use 
        enemyAnim = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    protected override BaseState GetInitialState() 
    {
        return idle;
    }
}
