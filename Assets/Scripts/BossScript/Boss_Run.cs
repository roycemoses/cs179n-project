using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss_Run : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public Vector2 initialPos;// get too far from this then disable/enable.
    public float returnDistance;
    private float speed2;
    private float speedoriginal;

    private Transform target;//Player

    private Animator animator;
    private double timer = 5;

    public bool fast = false;

    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        initialPos = gameObject.transform.position;
        speed2 = speed + 5;
        speedoriginal = speed;
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
        if (fast)
        {               
            animator.SetBool("isMoving", true);
            dir();
        }
        else{
            animator.SetBool("isMoving", false);
            dir();
        }
    }

    void dir(){
            Debug.Log(timer);

            
            if(fast){
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

                Vector2 direction = transform.position - target.position;
                Debug.Log("direction: " + direction);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
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

                
            if(timer <= 0){
                timer =2;
                fast = !fast;
            }
            else{
                timer -= Time.deltaTime;
            }
            
            
            
    }
        
}

    
   