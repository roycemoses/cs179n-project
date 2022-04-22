using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract so we dont assign it to something only used for inheritance 
public abstract class Character : MonoBehaviour
{
    //Speed of character
    [SerializeField]
    private float speed;

    protected Vector2 movement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(movement * speed * Time.deltaTime);
        AnimateMovement(movement);
    }
    public void AnimateMovement(Vector2 movement)
    {
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
    }
}
