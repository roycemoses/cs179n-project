using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttack : MonoBehaviour
{

    float nextAttackTime = 0f;

    public float surprisedSeconds = 1f;
    public float forcedIdleSeconds = 0.5f;
    public int damage = 15;
    public float followRange = 10f;
    public bool isAttacking;

    private Animator animator;

    public AudioSource meleeAttackSound;

    Vector3 playerPos;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerPos = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in enemy's attack range
        float distance = Vector2.Distance(playerPos, transform.position);
        if (distance > GetComponent<Enemy>().attackRange)
        {
            inRange = false;
        }
        else
        {
            inRange = true;
            if (animator.GetBool("Patrol Idle") || animator.GetBool("Patrol Moving"))
                animator.SetBool("Surprised Idle", true);
        }

        if (animator.GetBool("Patrol Idle"))
        {

        }
        else if (animator.GetBool("Patrol Moving"))
        {
            // patrol direction
            Vector2 patrolDirection = transform.position - moveSpots[randomSpot].position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (patrolDirection != Vector2.zero)
            {
                if ( ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) ) // RIGHT
                {
                    animator.SetFloat("Xinput", 1.0f);
                    animator.SetFloat("Yinput", 0f);
                }
                else if ( ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0)) ) // LEFT
                {
                    animator.SetFloat("Xinput", -1.0f);
                    animator.SetFloat("Yinput", 0f);
                }
                else if (angle <= 135 && angle >= 45) // DOWN
                {
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", -1.0f);              
                }
                else if (angle >= -135 && angle <= -45) // UP
                {
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", 1.0f); 
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            
            
        }
        else if (animator.GetBool("Forced Idle"))
        {
            // orc to player direction
            Vector2 direction = transform.position - playerPos;

        }
        












        if (Time.time >= nextAttackTime && inRange && !isAttacking)
        {
            Debug.Log("Attacking...");   
            StartCoroutine(Attack());   
        }
    }


    private IEnumerator Attack()
    {
        isAttacking = true;
        if (isSleeping)
        {
            Debug.Log("Surprised");
            isSleeping = false;
            animator.SetBool("isSleeping", false);
            yield return new WaitForSeconds(surprisedSeconds);
        }

        if (!inRange)
        {
            isPreparing = false;
            animator.SetBool("isPreparing", false);  
        }
        else
        {   
            isPreparing = true;
            animator.SetBool("isPreparing", true);  

            yield return new WaitForSeconds(chargeSeconds);

            Debug.Log("Attacking");
            isMoving = true;
            animator.SetBool("isMoving", true);
            isPreparing = false;
            animator.SetBool("isPreparing", false);

            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.drag = 0f;
            rb.AddForce((playerPos - transform.position).normalized * speed, ForceMode2D.Force);
        

            Vector2 direction = transform.position - playerPos;
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

            yield return new WaitForSeconds(movingSeconds);

            rb.drag = 3f;

            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        

        isInIdleState = true;
        animator.SetBool("isInIdleState", true);

        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        isAttacking = false;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("took dmg");
            col.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }
}
