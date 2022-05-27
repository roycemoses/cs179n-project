using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Vector2 initialPos;// get too far from this then disable/enable.
    public float returnDistance;
    Transform target;
    public float stoppingDistance;
    public float speed;
    private Animator animator;

    public void LookAtaPlayer(){
         if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {               
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            Vector2 direction = transform.position - target.position;
            Debug.Log("direction: " + direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            animator.SetBool("isMoving", true);

            if ( ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) ) // RIGHT
            {
                animator.SetFloat("Xinput", 1.0f);
                animator.SetFloat("Yinput", 0f);
                // Debug.Log("RIGHT");
            }
            else if ( ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0)) ) // LEFT
            {
                animator.SetFloat("Xinput", -1.0f);
                animator.SetFloat("Yinput", 0f);
                // Debug.Log("LEFT");
            }
            else if (angle <= 135 && angle >= 45) // DOWN
            {
                animator.SetFloat("Xinput", 0f);
                animator.SetFloat("Yinput", -1.0f);
                // Debug.Log("DOWN");               
            }
            else if (angle >= -135 && angle <= -45) // UP
            {
                animator.SetFloat("Xinput", 0f);
                animator.SetFloat("Yinput", 1.0f);
                // Debug.Log("UP");   
            }
        }
        else
            animator.SetBool("isMoving", false);

        if(Vector2.Distance(initialPos, gameObject.transform.position) >= returnDistance)
        {
            gameObject.GetComponent<Patrol2>().enabled = true;
            gameObject.GetComponent<Follow>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
