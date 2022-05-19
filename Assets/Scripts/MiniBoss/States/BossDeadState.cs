using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeadState : BaseState
{
    private Boss_FSM _bfsm;

    public BossDeadState(Boss_FSM statemachine) : base("dead", statemachine)
    {
        _bfsm = statemachine;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("BossTiles"); //create array of hidden tile gameobjects 
            
        foreach (GameObject bossHiddenTiles in tiles) //loop through hidden tile array
        {
            bossHiddenTiles.GetComponent<BossHiddenTiles>().RevealTiles(); //call the reveal method on each
            Debug.Log(tiles.Length);
        }  

        GameObject.Find("BossBlockerTrigger").SetActive(false); //disable the collider that locks in the player

        _bfsm.bossAnim.Play("dead"); //play dead anim
        _bfsm.bm.shouldFlip = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_bfsm.bossAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                GameObject.Destroy(GameObject.Find("Boss Health Bar"));
            }
    }
}