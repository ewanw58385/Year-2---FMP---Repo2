using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour //values declared that enemy FSM states use
{
    [HideInInspector]public Player_FSM psm;
    [HideInInspector]public float damageTaken;

    public LayerMask enemyLayer;

    public float playerHealth;
    public Vector2 knockbackDirection;

    public Transform weakAttackPos;
    public float weakAttackRange = 4f;
    public float weakAttackDamage;

    public Transform heavyAttackPos;
    public float heavtAttackRange = 4f;
    public float heavyAttackDamage;

    public float playerKnockbackForceX;
    public float playerKnockbackForceY; 

    // Start is called before the first frame update
    void Start()
    {
        weakAttackPos = transform.Find("PlayerWeakAttackPos");   
        psm = GetComponent<Player_FSM>();
    }

    public void TakeDamage(float damageTaken) //called in enemy attack script with damageTaken varying depending on the type of attack
    {
        playerHealth = playerHealth - damageTaken; //take away damage from health
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() //draw attack radius to visualise in Gizmos
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(weakAttackPos.position, weakAttackRange);
    }
}
