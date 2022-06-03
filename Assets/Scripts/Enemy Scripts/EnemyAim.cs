using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 position;
    Vector2 direction;
    float angle;

    // Update is called once per frame
    void Update()
    {
        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // mouseDirection = mousePos - rb.position;
        // angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        // if (Input.GetButtonDown("Fire1"))
        //     Debug.Log("angle: " + angle);

        position = GameObject.Find("Player").transform.position;
        transform.position = transform.parent.position;
    }
    
    void FixedUpdate()
    {
        direction = position - rb.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
