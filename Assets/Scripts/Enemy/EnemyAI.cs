using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target; //target for the AI to follow

    public float speed = 200f; //speed the AI will move
    public float nextWaypointDistance = 3f; //how close the enemy needs to be to a node before moving onto the next 
    public float aggroRange;
    public float attackRange;
    [HideInInspector]public bool playerWithinRange;
    [HideInInspector] public bool attackPlayer;

    public Path path; //Current path the AI is following
    [HideInInspector]public Seeker seeker; //Reference to the seeker script 

    private int currentWaypoint; //current waypoint along the path
    private bool reachedEndOfPath = false; //bool to check if path has been completed

    private Rigidbody2D rb; //Rigidbody2D reference for applying force to the AI. 
    private Transform enemyVFX;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange); //draws a sphere to visually see how close the player is before aggrovating 
        Gizmos.DrawWireSphere(transform.position, attackRange); //draws a sphere to visually see how close the player is before attacking
    }    

    // Start is called before the first frame update
    void Start()
    {
        enemyVFX = transform.GetChild(0);

        seeker = GetComponent<Seeker>(); 
        rb = GetComponent<Rigidbody2D>();

        //InvokeRepeating("UpdatePath", 0f, 0.2f); //keep updating the path, with no delay, every half second
        InvokeRepeating("CheckDistance", 0f, 0.2f); //checks the range of the player with no delay, every 0.2 seconds. 

    }
    
    void CheckDistance() //checks how close the player is to enemy and returns a boolean if close enough to attack (for the FSM to transition)
    {
        float dist = Vector2.Distance(rb.position, target.position); //returns distance between the target and the AI position
 
        if (dist <= aggroRange) //if the distance is smaller than the aggro range
        {
            playerWithinRange = true; //for FSM to transition 
        }
        else
        {
            playerWithinRange = false;
        }

        if (dist <= attackRange) //if player is within attack range
        {
            attackPlayer = true; //for FSM transition
        }
        else
        {
            attackPlayer = false;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) //if path has completed generation (so path is only generated when previous path has finished generating)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete); //Generates a path with the starting position (position of the AI), 
                                                                            //the end position of the path (AI target), and a method to be called once the path has been generated.
        }
    }

    void OnPathComplete(Path p) //takes a reference of the generated path
    {
        if (!p.error) //if path was correctly generated
        {
            path = p; //currently generated path = p
            currentWaypoint = 0; //reset progress along path (since generated new path)
        }
    }

    void Update()
    {
        if (path == null) //if there is not a path
        {
            return; //exit out of function
        }

        if (currentWaypoint >= path.vectorPath.Count) //if the current waypoint integer is greater or equal to the amount of nodes left on the current path 
                                                      // (if we have hit the end of the path)
        {
            reachedEndOfPath = true; //set bool 
            return; //exit
        }
        else //if more nodes to travel through 
        {
            reachedEndOfPath = false; //set bool
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]); //float value holding the distance between the AI's position and the next waypoint's position

        if (distance < nextWaypointDistance) //if the distance calculated is less than the nextWaypoint distance, the next waypoint has been reached and we can increase our current waypoint integer
        {
            currentWaypoint++; //increase current waypoint integer. 
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; //direction Vector = currentWaypoint's position on path - AI position (vector pointing in the direction of the target)
                                                                                                  // normalised so Vector values are never greater than 1. This is for applying force on the Ai as I will multiply this vector by a force value

        Vector2 force = direction * speed * Time.fixedDeltaTime; //create force vector for applying force to the AI in the correct direction
        rb.velocity = new Vector2(force.x, rb.velocity.y); //apply force to the AI    

        Flip(force); //flip the AI to face the player
    }

     void Flip(Vector2 force)
    {
        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            enemyVFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01 && force.x < 0f)
        {
            enemyVFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
