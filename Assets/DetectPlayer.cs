using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public MovePlatform mp;

    void OnTriggerStay2D (Collider2D col)
    {
        mp = GetComponentInParent<MovePlatform>();
        
        if (col.tag == "Player")
        {
            mp.shouldMove = true;
        }
    }
}
