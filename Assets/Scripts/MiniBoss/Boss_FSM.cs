using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FSM : G_FSM
{
    [HideInInspector] public BossIdleState idle;
    [HideInInspector] public BossMovingState moving;
    [HideInInspector] public BossWeakAttack weakattack;
    [HideInInspector] public BossHeavyAttack heavyattack;
    [HideInInspector] public BossHitState hit;

    [HideInInspector] public Animator bossAnim;
    [HideInInspector] public Rigidbody2D bossRb;
    [HideInInspector] public BossAI bossAI;
    [HideInInspector] public BossCombatManager bcm;
    [HideInInspector] public BossManager bm;
    [HideInInspector] public GameObject player;

    [HideInInspector] public bool hitCondition; //condition for transitioning to hit state, set to true on Player attack state 
    [HideInInspector] public float damageTaken; //float for holding the amount of damage taken. declared on player attack state, passed as a parameter within Hit state
    [HideInInspector] public bool enemyDead; //bool for transitioning to dead state. Determined in the boss combat manager. 

    public void Awake()
    {
        idle = new BossIdleState(this);
        moving = new BossMovingState(this);
        weakattack = new BossWeakAttack(this);
        heavyattack = new BossHeavyAttack(this);
        hit = new BossHitState(this);

        bossAI = GetComponent<BossAI>();
        bossRb = GetComponent<Rigidbody2D>();
        bossAnim = GetComponentInChildren<Animator>();
        bcm = GetComponent<BossCombatManager>();
        bm = GetComponent<BossManager>();
        player = GameObject.Find("Player");


    }

    protected override BaseState GetInitialState() 
    {
        return idle;
    }
}