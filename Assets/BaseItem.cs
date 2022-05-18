using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{

    public virtual void Initialise()
    { 
        Debug.Log("found an item");
    }
}
