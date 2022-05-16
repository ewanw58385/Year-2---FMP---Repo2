using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour //values declared that enemy FSM states use
{
    [HideInInspector] public Player_FSM psm; //player state machine
    [HideInInspector] public HealthBarScript healthBar; //reference to healthbar

    public float playerHealth; //health set in inspector
    [HideInInspector] public float playerStartingHealth; //starting health as reference for stopping recovery timer
    [HideInInspector] public float currentHealth; //for manipulating health value

    [HideInInspector] public float recoveryDecrease; //timer decrease 
    public float recoveryDelayTime; //recovery time delay
    public float recoverySpeed;
    private bool recoveryTimer = false; //bool to start recovery

    public LayerMask enemyLayer; //for detecting enemies 

    public Transform weakAttackPos; //weak attack position
    public float weakAttackRange = 4f; //radius of attack
    public float weakAttackDamage; //damage

    public Transform heavyAttackPos; 
    public float heavyAttackRange = 4f;
    public float heavyAttackDamage;

    // Start is called before the first frame update
    void Start()
    {
        psm = GetComponent<Player_FSM>();

        playerStartingHealth = playerHealth; //set max health
        recoveryDecrease = recoveryDelayTime;

        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBarScript>(); //get instance of UI healthbar script
        healthBar.SetMaxHealth(playerHealth); //pass Startingalth (max health) as max health on slider
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

    public void TakeDamage(float damageTaken) //called in enemy attack script with damageTaken varying depending on the type of attack
    {
        recoveryTimer = true; //if takes damage, begin recovery timer in update
        recoveryDecrease = recoveryDelayTime; //reset recovery delay

        currentHealth = playerHealth - damageTaken; //take away damage from health
        playerHealth = currentHealth; //set player health to current health for next attack

        healthBar.SetHealth(currentHealth); //set new value in healthbar script
    }

    public void RestoreHealth()
    {
        currentHealth = currentHealth + recoverySpeed * Time.deltaTime; //increase current health by 5 every second
        playerHealth = currentHealth; //set player health value
        healthBar.SetHealth(currentHealth); //set new value in healthbar script

        if(currentHealth >= playerStartingHealth) //if health has reached max (starting health)
        {
            recoveryTimer = false; //reset recovery timer
            return; //return out of function
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() //draw attack radius to visualise in Gizmos
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(weakAttackPos.position, weakAttackRange);
        Gizmos.DrawWireSphere(heavyAttackPos.position, heavyAttackRange);
    }
}
