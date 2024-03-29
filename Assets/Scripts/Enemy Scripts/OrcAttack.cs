using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttack : MonoBehaviour
{

    float nextAttackTime = 0f;

    
    public float surprisedSeconds;
    public float startSurprisedSeconds;
    public float forcedIdleSeconds;
    public float waitTime;
    public float startWaitTime;
    public float chargeSeconds;
    public float attackAnimationTime;
    public float detectRange;
    public bool isAttacking;
    public bool inRange;
    public float speed;

    public List<Transform> moveSpots;
    private int randomSpot;

    private Animator animator;

    public AudioSource meleeAttackSound;

    public GameObject player;

    private Vector3 playerPos;

    public GameObject hitboxPrefab;
    public float startHitboxDistance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Count);
        if (player == null)
        {
            Debug.Log("Error: Enemy cannot find player");
        }

        if (moveSpots.Count == 0)
        {
            GameObject topLeft = new GameObject();
            topLeft.transform.position = transform.position + new Vector3(-2, 2, 0);
            GameObject topRight = new GameObject();
            topRight.transform.position = transform.position + new Vector3(2, 2, 0);
            GameObject bottomLeft = new GameObject();
            bottomLeft.transform.position = transform.position + new Vector3(-2, -2, 0);
            GameObject bottomRight = new GameObject();
            bottomRight.transform.position = transform.position + new Vector3(2, -2, 0);
            GameObject center = new GameObject();
            center.transform.position = transform.position;            
            moveSpots.Add(topLeft.transform);
            moveSpots.Add(topRight.transform);
            moveSpots.Add(bottomLeft.transform);
            moveSpots.Add(bottomRight.transform);
            moveSpots.Add(center.transform);
        }
        animator.SetBool("Patrol Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in enemy's attack range
        playerPos = player.transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        if (distance > GetComponent<Enemy>().attackRange)
        {
            inRange = false;
        }
        else
        {
            inRange = true;
            if (animator.GetBool("Patrol Idle") || animator.GetBool("Patrol Moving"))
            {
                animator.SetBool("Surprised Idle", true);
                animator.SetBool("Patrol Idle", false);
                animator.SetBool("Patrol Moving", false);
            }
        }

        if (animator.GetBool("Patrol Idle"))
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Count);
                animator.SetBool("Patrol Moving", true);
                animator.SetBool("Patrol Idle", false);
            }
            if (Vector2.Distance(playerPos, transform.position) <= detectRange) // If the Player gets too close to Enemy!
            {
                // Go to Surprised state and reset the wait time!
                animator.SetBool("Surprised Idle", true);
                animator.SetBool("Patrol Idle", false);
            }
        }
        else if (animator.GetBool("Patrol Moving"))
        {
            // look direction
            Vector2 lookDirection = transform.position - moveSpots[randomSpot].position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            if (lookDirection != Vector2.zero)
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
            
            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) <= 0.2f)
            {
                waitTime = startWaitTime;
                animator.SetBool("Patrol Idle", true);
                animator.SetBool("Patrol Moving", false);
            }

            if (Vector2.Distance(playerPos, transform.position) <= detectRange) // If the Player gets too close to Enemy!
            {
                // Go to Surprised state and reset the wait time!
                animator.SetBool("Surprised Idle", true);
                animator.SetBool("Patrol Moving", false);
            }
        }
        else if (animator.GetBool("Surprised Idle"))
        {
            waitTime = startWaitTime;

            Vector2 lookDirection = transform.position - playerPos;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

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

            if (surprisedSeconds <= 0)
            {
                animator.SetBool("Following", true);
                animator.SetBool("Surprised Idle", false);
            }
            else
            {
                surprisedSeconds -= Time.deltaTime;
            }
        }
        else if (animator.GetBool("Following"))
        {            
            surprisedSeconds = startSurprisedSeconds;

            Vector2 moveDirection = transform.position - playerPos;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

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

            if (Vector2.Distance(playerPos, transform.position) >= detectRange) // The player got too far away! Follow range exceeded; back to patrol
            {
                animator.SetBool("Following", false);
                animator.SetBool("Patrol Moving", true);
            }
            
            if (Vector2.Distance(playerPos, transform.position) > GetComponent<Enemy>().attackRange) // if the distance between the orc and player is too far to attack, orc follows!
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

            if (Time.time >= nextAttackTime && Vector2.Distance(playerPos, transform.position) <= GetComponent<Enemy>().attackRange && !isAttacking) // The Orc has no cooldown, Orc is close enough, and is not attacking: Start the Attack Coroutine!
            {
                Debug.Log("Attacking...");
                animator.SetBool("Following", false);
                animator.SetBool("Preparing", true);
                StartCoroutine(Attack());
            }
        }
        else if (animator.GetBool("Forced Idle"))
        {
            surprisedSeconds = startSurprisedSeconds;
            // orc to player direction
            // Vector2 direction = transform.position - playerPos;
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        // TODO: Spawn outline of real hitbox!

        Vector3 lookDirection = transform.position - playerPos; // Look at the player while charging
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;  
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

        Vector3 pos = playerPos;
        GameObject hitbox = Instantiate(hitboxPrefab, pos, Quaternion.Euler(0, 0, angle+90f));
        hitbox.transform.parent = gameObject.transform;
        hitbox.GetComponent<BoxCollider2D>().enabled = false;
        hitbox.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.55f); // change opacity to 55%

        yield return new WaitForSeconds(chargeSeconds);

        animator.SetBool("Preparing", false);
        animator.SetBool("Attacking", true);
        
        // TODO: Hitbox ACTIVATES (enable collider) and deals damage to the player.
        hitbox.GetComponent<BoxCollider2D>().enabled = true;
        hitbox.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); // change opacity to 0%

        yield return new WaitForSeconds(attackAnimationTime);

        animator.SetBool("Forced Idle", true);
        animator.SetBool("Attacking", false);

        Destroy(hitbox);

        yield return new WaitForSeconds(forcedIdleSeconds);

        if (inRange)
            animator.SetBool("Following", true);
        else
            animator.SetBool("Patrol Moving", true);
        
        animator.SetBool("Forced Idle", false);

        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        isAttacking = false;
    }

    // private void OnTriggerEnter2D(Collider2D col) {
    //     if (col.CompareTag("Player"))
    //     {
    //         Debug.Log("took dmg");
    //         col.gameObject.GetComponent<PlayerManager>().TakeDamage(GetComponent<Enemy>().damage);
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Player"))
        {
            Debug.Log("took dmg");
            col.collider.gameObject.GetComponent<PlayerManager>().TakeDamage(GetComponent<Enemy>().damage);
        }
    }
}
