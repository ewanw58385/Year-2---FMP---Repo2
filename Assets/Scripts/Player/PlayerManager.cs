using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;

    [SerializeField] private LayerMask ground;
    public LayerMask interactable;

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
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("HiddenTiles"); //create array of hidden tile gameobjects 
            
            foreach (GameObject hiddenTiles in tiles) //loop through hidden tile array
            {
                hiddenTiles.GetComponent<HiddenTiles>().RevealTiles(); //call the reveal method on each
            }  
            
            Destroy(GameObject.FindWithTag("revealItem")); //destroy item pickup     
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;
        //Debug.Log("not grounded");
    }

    public void CheckForItemInteraction()
    {
        Collider2D[] ItemsWithinCollider = Physics2D.OverlapCircleAll(pickupPos.position, pickupRange, interactable); //creates an array of colliders checking if there is an item pickup close (called in idle state) 
        foreach (Collider2D itemInCollider in ItemsWithinCollider)
        {
            itemInCollider.GetComponent<ItemPickup>().Initialise(); //calls initialise method on item pickup
        }
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
