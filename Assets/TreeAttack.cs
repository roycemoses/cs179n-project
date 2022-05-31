using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttack : MonoBehaviour
{

    float nextAttackTime = 0f;
    float movingTime = 0f;
    float chargeTime = 2f;
    public float speed = 250f;
    public float movingTimeFactor = 3f;
    public int damage;

    private Animator animator;

    public AudioSource projectileSound;

    bool inRange = false;
    public bool isMoving;
    public bool isPreparing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in enemy's attack range
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (distance > GetComponent<Enemy>().attackRange)
            inRange = false;
        else
            inRange = true;

        if (inRange)
        {
            animator.SetBool("isSleeping", false);
            isPreparing = true;
            animator.SetBool("isPreparing", true);
            chargeTime -= Time.deltaTime;
            if (chargeTime < 0)
            {   
                isPreparing = false;
                animator.SetBool("isPreparing", false);
            }
        }
        
        if (!inRange)
        {
            animator.SetBool("isSleeping", true);
            isPreparing = false;
            animator.SetBool("isPreparing", false);
        }


        if (Time.time >= nextAttackTime && inRange && !isPreparing)
        {
            Debug.Log("Attacking");
            projectileSound.Play();
            nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
            movingTime = Time.time + 1f / (GetComponent<Enemy>().attackRate * movingTimeFactor);
            rb.drag = 0f;
            rb.AddForce((playerPos - transform.position).normalized * speed, ForceMode2D.Force);
            
            isMoving = true;
            animator.SetBool("isMoving", true);
            chargeTime = 2f;

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
        }

        if (Time.time >= movingTime && isMoving)
        {
            Debug.Log("Drag!");
            rb.drag = 3f;
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
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
