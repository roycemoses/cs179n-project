using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public Rigidbody2D rb;

    Vector2 movement;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x == 1)
        {
            Debug.Log("right");
            animator.SetBool("UserInput_D", true);
        }
        else
        {
            animator.SetBool("UserInput_D", false);
        }

        if (movement.x == -1)
        {
            Debug.Log("left");
            animator.SetBool("UserInput_A", true);
        }
        else
        {
            animator.SetBool("UserInput_A", false);
        }

        if (movement.y == 1)
        {
            Debug.Log("up");
            animator.SetBool("UserInput_W", true);
        }
        else
        {
            animator.SetBool("UserInput_W", false);
        }
        
        if (movement.y == -1)
        {
            Debug.Log("down");
            animator.SetBool("UserInput_S", true);
        }
        else
        {
            animator.SetBool("UserInput_S", false);
        }
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
