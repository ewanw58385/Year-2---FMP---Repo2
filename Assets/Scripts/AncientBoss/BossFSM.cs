using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FSM : G_FSM
{
    [HideInInspector] public BossIdleState idle;

    public Animation bAnim;
    public Rigidbody2D bRb;
    public BossAI bossAI;

    public void Awake()
    {
        idle = new BossIdleState(this);

        bossAI = GetComponent<BossAI>();
    }

    protected override BaseState GetInitialState() 
    {
        return idle;
    }
}