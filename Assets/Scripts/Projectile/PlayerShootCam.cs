using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCam : MonoBehaviour
{  
    public Rigidbody2D rb;
    Vector2 mousePos;
    Vector2 mouseDirection;
    float angle;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // mouseDirection = mousePos - rb.position;
        // angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        // if (Input.GetButtonDown("Fire1"))
        //     Debug.Log("angle: " + angle);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = transform.parent.position;
    }
    
    void FixedUpdate()
    {
        mouseDirection = mousePos - rb.position;
        angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
