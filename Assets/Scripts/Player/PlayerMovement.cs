using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePos;

    private Animator animator;

    public Camera cam;
    public VectorValue startingPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Xinput", movement.x);
            animator.SetFloat("Yinput", movement.y);
        }

        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Vector2 mouseDirection = mousePos - rb.position;
        // float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        // if (Input.GetButtonDown("Fire1"))
        //     Debug.Log("angle: " + angle);
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }
}
