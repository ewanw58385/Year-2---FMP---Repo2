using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState //Specific states will inherit from this class to use the Enter, Update and Exit functions. Does not inherit from monobehaviour as is not being instantiated at runtime (on an object).  
{   
    public string stateName; //name of the state
    protected G_FSM stateMachine; //stores the StateMachine class as an object

    public BaseState(string stateName, G_FSM stateMachine) // parent/base constructor (gets initialised when an object of this class gets instantiated) for each child state/ takes stateName and... 
                                                                  //...StateMachine parameters. This is for each individual state to initialise with the correct name, and sub-state machine it is going to use. 
                                                                  //...This is specified within each state's class.
    {
        this.stateName = stateName; //this (refering to the "stateName" declared in this class and NOT the "stateName" being passed in) stores the stateName being passed in
        this.stateMachine = stateMachine; //same as above 
    }

    public virtual void Enter(){} //Enter, Update and Exit funcitons to be used in individual state's class. These get run in the parent StateMachine 
    public virtual void UpdateLogic(){}
    public virtual void UpdatePhysics(){}
    public virtual void Exit(){}
}
