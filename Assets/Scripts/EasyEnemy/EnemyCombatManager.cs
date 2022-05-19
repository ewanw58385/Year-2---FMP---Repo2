using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Enemy_FSM _EFSM;
    public EnemyHealthBarScript healthBar;

    public LayerMask playerLayerMask;
    
    public Transform weakAttackPosition; 
    public float weakAttackRange; 
    public float weakAttackDamage;

    public Transform heavyAttackPosition;
    public float heavyAttackRange;
    public float heavyAttackDamage;

    public float enemyHealth = 9;
    public float currentEnemyHealth;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _EFSM = GetComponent<Enemy_FSM>();

        //healthBar = transform.Find("Enemy Health Bar").GetComponent<EnemyHealthBarScript>(); //get instance of UI healthbar script
        healthBar.SetMaxHealth(enemyHealth); //pass initial health (max health) as max health on slider
    }

    void Update()
    {
        //Debug.Log(enemyHealth);
    }
    
    public void ApplyDamage(float damageTaken)
    {                
        currentEnemyHealth =  enemyHealth - damageTaken; //deduct damage to health
        enemyHealth = currentEnemyHealth;

        if (currentEnemyHealth <= 0)
        {
            _EFSM.enemyDead = true;
        }

        healthBar.SetHealth(currentEnemyHealth); //set new value in healthbar script
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(weakAttackPosition.position, weakAttackRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(heavyAttackPosition.position, heavyAttackRange);
    }
}
