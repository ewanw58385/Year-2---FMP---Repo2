using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Boss_FSM _bfsm;
    [HideInInspector] public BossHealthbarScript healthBar;


    public LayerMask playerLayerMask;
    
    public Transform weakAttackPosition; 
    public float weakAttackRange; 
    public float weakAttackDamage;

    public Transform heavyAttackPosition;
    public float heavyAttackRange;
    public float heavyAttackDamage;

    public float bossHealth = 100;
    public float currentBossHealth;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _bfsm = GetComponent<Boss_FSM>();

        healthBar = GameObject.Find("Boss Health Bar").GetComponent<BossHealthbarScript>(); //get instance of UI healthbar script
        healthBar.SetMaxHealth(bossHealth); //pass initial health (max health) as max health on slider
    }
    
    public void ApplyDamage(float damageTaken)
    {                
        currentBossHealth =  bossHealth - damageTaken; //deduct damage to health
        bossHealth = currentBossHealth;

        if (currentBossHealth <= 0)
        {
            _bfsm.enemyDead = true;
        }

        healthBar.SetHealth(currentBossHealth); //set new value in healthbar script
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