using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public bool shouldFlip = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFlip) //bool set in states
        {
            FlipSprite();
        }
    }

    public void FlipSprite()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3 (1, 1, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3 (-1, 1, 1);
        }
    }
}
