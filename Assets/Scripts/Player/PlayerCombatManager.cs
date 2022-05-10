using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour //values declared that enemy FSM states use
{
    public LayerMask enemyLayer;

    public Transform weakAttackPos;
    public float weakAttackRange = 4f;
    public float weakAttackDamage;

    public Transform heavyAttackPos;
    public float heavtAttackRange = 4f;
    public float heavyAttackDamage;


    // Start is called before the first frame update
    void Start()
    {
        weakAttackPos = transform.Find("weakAttackPos");   
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(weakAttackPos.position, weakAttackRange);
    }
}
