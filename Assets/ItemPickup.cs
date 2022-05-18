using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : BaseItem
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject boss;

    [HideInInspector] public Animator chestGFX;
    public Animator chestVFX;

    void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("MiniBoss");

        chestGFX = GetComponent<Animator>();
    }

    public override void Initialise()
    {   
        if (boss.GetComponent<Boss_FSM>().bossDead)
        {
            player.GetComponent<Player_FSM>().doubleJumpUnlocked = true; //allow player to double jump
            chestGFX.Play("chestOpen");
            chestVFX.SetTrigger("chestOpened");

            Debug.Log("opened a chest");
        }        
    }
}
