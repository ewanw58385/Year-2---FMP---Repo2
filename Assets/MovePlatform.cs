using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i; 
    [HideInInspector] public bool shouldMove = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldMove)
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.2f)
            {
                i++; //move onto next point
                if (i == points.Length)
                {
                    i = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        col.transform.SetParent(transform);
    }

    void OnCollisionExit2D (Collision2D col)
    {
        col.transform.SetParent(null);
    }
}
