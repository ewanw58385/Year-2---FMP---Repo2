using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FSM : MonoBehaviour
{

    BaseState currentState; //stores the current state of BaseState type (class)

    void Start() 
    {
        currentState = GetInitialState(); //first get inital state and store as currentState

        if(currentState != null) //if there is a state active
        {
            currentState.Enter(); //run the enter function for that state
        }
    }
    
    void Update()
    {
        if(currentState != null)  //if there is a state active
        {
            currentState.UpdateLogic(); //run the update function for that state
        }
    }

    void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseState newState) //ChangeState method takes a new state of type BaseState (class)
    {
            currentState.Exit(); //exit current state 

            currentState = newState; //store new state
            currentState.Enter(); //ener new state
    }
  
    protected virtual BaseState GetInitialState() //ruturns null because specified state is not set. Will be set within the movement sub-state machine 
    {
        return null;
    }

        private void OnGUI()
    {
        string content = currentState != null ? currentState.stateName : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
