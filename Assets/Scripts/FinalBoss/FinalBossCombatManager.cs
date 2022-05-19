using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCombatManager : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Boss_FSM _fbsm;
    [HideInInspector] public FinalBossHealthbarScript healthBar; //gets initiated in trigger script since gameobject is disabled until collision trigger


    public LayerMask playerLayerMask;
    
    public Transform weakAttackPosition; 
    public float weakAttackRange; 
    public float weakAttackDamage;

    public Transform heavyAttackPosition;
    public float heavyAttackRange;
    public float heavyAttackDamage;

    public Transform specialAttackPosition; 
    public float specialAttackRange; 
    public float specialAttackDamage;

    [HideInInspector] public float bossStartingHealth;
    [HideInInspector] public float currentBossHealth;
    public float bossHealth;

    [HideInInspector] public float recoveryDecrease; //timer decrease 
    public float recoveryDelayTime; //recovery time delay
    public float recoverySpeed;
    private bool recoveryTimer = false; //bool to start recovery


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _fbsm = GetComponent<Boss_FSM>(); 
        
        bossStartingHealth = bossHealth; //set max health
        recoveryDecrease = recoveryDelayTime;
    }
    
    void Update()
    {
        if (recoveryTimer) //if taken damage (recovery timer has been initiated)
        {
            recoveryDecrease -= Time.deltaTime; //begin decreasing timer

            if(recoveryDecrease <= 0f) //if timer = 0
            {
                RestoreHealth(); //restore health
            }
        }
    }

    public void RestoreHealth()
    {
        currentBossHealth = currentBossHealth + recoverySpeed * Time.deltaTime; //increase current health by 5 every second
        bossHealth = currentBossHealth; //set player health value
        healthBar.SetHealth(currentBossHealth); //set new value in healthbar script

        if(currentBossHealth >= bossStartingHealth) //if health has reached max (starting health)
        {
            recoveryTimer = false; //reset recovery timer
            return; //return out of function
        }
    }

    public void ApplyDamage(float damageTaken)
    {                
        recoveryTimer = true;
        recoveryDecrease = recoveryDelayTime; //reset recovery delay
        
        currentBossHealth =  bossHealth - damageTaken; //deduct damage to health
        bossHealth = currentBossHealth;

        if (currentBossHealth <= 0)
        {
            _fbsm.bossDead = true;
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
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(specialAttackPosition.position, specialAttackRange);
    }
}