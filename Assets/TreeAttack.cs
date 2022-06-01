using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttack : MonoBehaviour
{
    public Color chargeUpColor;
    float nextAttackTime = 0f;

    public float surprisedSeconds = 1f;
    public float chargeSeconds = 1.5f;
    public float chargeUpDeltaTime = 0.3f;
    public float movingSeconds = 1f;
    public float idleAfterAttackSeconds = 0.5f;
    public float speed = 250f;
    public int damage = 15;

    private Animator animator;

    public AudioSource projectileSound;

    public bool inRange = false;
    public bool isAttacking = false;
    public bool isMoving = false;
    public bool isPreparing = false;
    public bool isSleeping = true;
    public bool isSurprised = false;
    public bool isInIdleState = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in enemy's attack range
        Vector3 playerPos = player.transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        
        if (isPreparing) {
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

        if (distance > GetComponent<Enemy>().attackRange)
        {
            inRange = false;
            if (isInIdleState)
            {
                isSleeping = true;
                animator.SetBool("isSleeping", true);
                isInIdleState = false;
                animator.SetBool("isInIdleState", false);
                nextAttackTime = 0f;
            }
        }
        else
        {
            inRange = true;
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

            // GetComponent<SpriteRenderer>().color = orange;
            // yield return new WaitForSeconds(chargeUpDurationSeconds);
            for (float i = 1f; i >= 0; i -= chargeUpDeltaTime)
            {
                Color c = GetComponent<SpriteRenderer>().color;
                if (i >= (chargeUpColor.r * 255f) / 255f)
                    c.r = i;
                if (i >= (chargeUpColor.g * 255f) / 255f)
                    c.g = i;
                if (i >= (chargeUpColor.b * 255f) / 255f)
                    c.b = i;
                GetComponent<SpriteRenderer>().color = c;
                yield return new WaitForSeconds(chargeUpDeltaTime);
            }
            GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;

            Debug.Log("Attacking");
            isMoving = true;
            animator.SetBool("isMoving", true);
            isPreparing = false;
            animator.SetBool("isPreparing", false);

            projectileSound.Play();
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.drag = 0f;
            rb.AddForce((playerPos - transform.position).normalized * speed, ForceMode2D.Force);     

            yield return new WaitForSeconds(movingSeconds);

            rb.drag = 3f;

            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        
        yield return new WaitForSeconds(idleAfterAttackSeconds);

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
