using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    //keep track of current state
    public State currentState{get;private set;}

    //initialize function
    public void Initialize(State startingState){
        currentState = startingState;
        currentState.Enter();
    }

    //changes state we are currently in 
    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
