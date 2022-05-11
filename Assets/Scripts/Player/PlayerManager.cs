using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;
    [SerializeField] private LayerMask ground;

    private float horiInput;
    public float scaleOfPlayer;

    void Update()
    {
        horiInput = Input.GetAxis("Horizontal");
        Flip(horiInput);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "floor" || col.gameObject.tag == "HiddenTiles") //checking if the player is standing on the floor or a hidden tile 
        {
            isGrounded = true;
            //Debug.Log("grounded");
        }

        if (col.gameObject.tag == "revealItem") //if touches item pickup
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("HiddenTiles"); //reveal tiles
            
            foreach (GameObject hiddenTiles in tiles) //loop through hidden tiles
            {
                hiddenTiles.GetComponent<Reveal>().RevealTiles();
                //Debug.Log(tiles.Length);
            }  
            Destroy(GameObject.FindWithTag("revealItem")); //destroy item pickup     
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;
        //Debug.Log("not grounded");
    }
    
    void Flip(float flipDirection)
    {
        if (flipDirection > 0) //move left
        {
            transform.localScale = new Vector2 (scaleOfPlayer, scaleOfPlayer);
        }

        if (flipDirection < 0) //move right
        {
            transform.localScale = new Vector2 (-scaleOfPlayer, scaleOfPlayer); //flip 
        }
    }
}
