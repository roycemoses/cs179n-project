using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public int facingDirection{get;private set;}
    public Animator anim{get;private set;}
    public GameObject aliveGO {get;private set;}

    private Vector2 velocityWorkspace;
    public virtual void Start(){
        aliveGO = transform.Find("Alive").gameObject;
        anim = aliveGO.GetComponent<Animator>(); 

        stateMachine = new FiniteStateMachine();
    }
    public virtual void Update(){
        stateMachine.currentState.Logicupdate();
    }

    public virtual void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity){
        // velocityWorkspace.Set(facingDirection*velocity);
        
    }
}
