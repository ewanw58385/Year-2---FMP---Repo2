using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Enemy_FSM _EFSM;

    public LayerMask playerLayerMask;
    
    public Transform weakAttackPosition; 
    public float weakAttackRange = 0.8f; 
    public float weakAttackDamage;

    public Transform heavyAttackPosition;
    public float heavyAttackRange;
    public float heavyAttackDamage;

    public float enemyHealth = 9;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _EFSM = GetComponent<Enemy_FSM>();

        weakAttackPosition = transform.Find("weakAttackPos");
    }
    
    public void ApplyDamage(float damageTaken)
    {                
        enemyHealth =  enemyHealth - damageTaken; //deduct damage to health

        if (enemyHealth <= 0)
        {
            _EFSM.enemyDead = true;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(weakAttackPosition.position, weakAttackRange);
    }
}
