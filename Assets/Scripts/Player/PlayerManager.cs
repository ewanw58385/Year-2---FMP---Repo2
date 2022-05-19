using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool isGrounded;

    public GameObject pickupPos;
    public float pickupRange = 5f;

    [SerializeField] private LayerMask ground;
    public LayerMask interactable;

    private float horiInput;
    public float scaleOfPlayer;

    void Start()
    {
        pickupPos = GameObject.Find("PlayerPickupPos");
    }

    void Update()
    {
        horiInput = Input.GetAxis("Horizontal");
        Flip(horiInput);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "floor" || col.gameObject.tag == "HiddenTiles" || col.gameObject.tag == "BossTiles" || col.gameObject.tag == "walkableProps") //checking if the player is standing on a walkable surface
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
        Collider2D pickupItem = Physics2D.OverlapCircle(pickupPos.transform.position, pickupRange, interactable); //checks for item pickup within range (method called in idle state) 
        pickupItem.GetComponent<BaseItem>().Initialise(); //calls BaseItem initialise method, child Instantiate Method gets called specific to that item
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pickupPos.transform.position, pickupRange);
    }
}
