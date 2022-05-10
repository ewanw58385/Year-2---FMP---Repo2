using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Enemy_FSM _EFSM;

    public float enemyHealth = 9;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _EFSM = GetComponent<Enemy_FSM>();
    }
    
    public void ApplyDamage(float damageTaken)
    {                
        enemyHealth =  enemyHealth - damageTaken; //deduct damage to health

        Debug.Log(enemyHealth);
        
        if (enemyHealth <= 0)
        {
            _EFSM.enemyDead = true;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
