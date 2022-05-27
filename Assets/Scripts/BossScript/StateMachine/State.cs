using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    //all variables that state needs to keep track of
    //what finiste state machine it belongs to
    protected FiniteStateMachine stateMachine;

    //what entity it belongs to
    protected Entity entity;

    //what is the starting time the enemy entered the state
    protected float startTime;

    protected string animBoolName;

    //constructor
    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName){
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter(){
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit(){
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void Logicupdate(){

    }

    public virtual void PhysicsUpdate(){

    }
}
