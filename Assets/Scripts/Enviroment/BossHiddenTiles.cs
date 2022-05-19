using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHiddenTiles : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D col;
    [HideInInspector] public SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        col.enabled = false; //on instantiation, disable collider
        sr.enabled = false; //disable renderer     
    }

    public void RevealTiles() //method to be called in player pickup script. Did not want to instantiate the tiles as each tile would then need it's own position/ gameobject's transform manually passed 
    {
        sr.enabled = true; //enable renderer 
        anim.Play("fadeIn"); //reveal the tiles
        col.enabled = true; //enable the collider

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.2f)
        {
            anim.SetTrigger("glow");
        }
    }
}
