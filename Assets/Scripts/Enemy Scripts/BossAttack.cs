using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    float nextAttackTime = 0f;

    public float chargeUpDeltaTime;

    public float treeAttackChargeSeconds;
    public float treeAttackMovingSeconds;
    public float treeAttackMovementSpeed;
    public float treeAttackDragForceAfterMovement;
    public Color treeChargeUpColor;
    public float orcAttackChargeSeconds;
    public float orcAttackAnimationTime;
    public float orcAttackHitboxAngle; // 45
    public float orcAttackHitboxMagnitude; // 1.5
    public float orcAttackHitboxTilt; // 25
    public Color orcChargeUpColor;
    public float villainAttackProjectileChargeTime;
    public float villainAttackSecondsBetweenBursts;
    public int villainAttackNumberOfBursts;
    public int villainAttackNumberOfProjectilesPerBurst;
    public Color villainChargeUpColor;

    public float forcedIdleAfterAttackingSeconds;

    private Animator animator;

    public GameObject orcAttackHitboxPrefab;

    // public AudioSource projectileSound;

    public bool inRange;
    public bool isAttacking;
    public bool isPreparing;
    public bool isInIdleState;
    public GameObject player;
    private Vector3 playerPos;
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
        playerPos = player.transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        
        if (animator.GetBool("Tree Preparing")) {
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

        if (Time.time >= nextAttackTime && !isAttacking)
        {
            // generate a random number!
            int randomAttack = Random.Range(0, 3); // IF TESTING: change this to anything you want~~~~~~~
            if (randomAttack == 0) // TREE ATTACK
            {
                Debug.Log("Start attack coroutine: Preparing");
                StartCoroutine(TreeAttack());   
            }
            else if (randomAttack == 1)
            {
                Debug.Log("Start ORC attack coroutine");
                StartCoroutine(OrcAttack());
            }
            else if (randomAttack == 2)
            {
                Debug.Log("Start Villain attack coroutine");
                StartCoroutine(VillainAttack());
            }
        }
    }

    private IEnumerator VillainAttack()
    {
        isAttacking = true;

        for (float i = 1f; i >= 0; i -= chargeUpDeltaTime)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            if (i >= (villainChargeUpColor.r * 255f) / 255f)
                c.r = i;
            if (i >= (villainChargeUpColor.g * 255f) / 255f)
                c.g = i;
            if (i >= (villainChargeUpColor.b * 255f) / 255f)
                c.b = i;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(chargeUpDeltaTime);
        }
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;


        Transform firePoint = transform.Find("EnemyFirepoint").transform;
        GameObject firePointLock = new GameObject();
        firePointLock.transform.position = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
        firePointLock.transform.Rotate(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z);


        gameObject.GetComponentInChildren<BossShoot>().Shoot(0f, firePointLock.transform);
        yield return new WaitForSeconds(villainAttackSecondsBetweenBursts);
        gameObject.GetComponentInChildren<BossShoot>().Shoot(22.5f, firePointLock.transform);
        yield return new WaitForSeconds(villainAttackSecondsBetweenBursts);
        gameObject.GetComponentInChildren<BossShoot>().Shoot(0f, firePointLock.transform);
        yield return new WaitForSeconds(villainAttackSecondsBetweenBursts);
        gameObject.GetComponentInChildren<BossShoot>().Shoot(22.5f, firePointLock.transform);
        yield return new WaitForSeconds(villainAttackSecondsBetweenBursts);
        gameObject.GetComponentInChildren<BossShoot>().Shoot(0f, firePointLock.transform);

        // bool fireOnPlayer = true;

        // for (int i = 0; i < villainAttackNumberOfBursts; ++i)
        // {
        //     if (fireOnPlayer)
        //         gameObject.GetComponentInChildren<BossShoot>().Shoot(0f, firePointLock.transform);
        //     else
        //         gameObject.GetComponentInChildren<BossShoot>().Shoot(22.5f, firePointLock.transform);
            
        //     fireOnPlayer = !fireOnPlayer;
        //     if (i < villainAttackNumberOfBursts - 1)
        //         yield return new WaitForSeconds(villainAttackSecondsBetweenBursts);
        // }

        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;

        isAttacking = false;
    }

    private IEnumerator OrcAttack()
    {
        isAttacking = true;

        // Spawn outline of real hitbox!

        animator.SetBool("Forced Idle", false);
        animator.SetBool("Orc Preparing", true);

        // charge up orc attack
        for (float i = 1f; i >= 0; i -= chargeUpDeltaTime)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            if (i >= (orcChargeUpColor.r * 255f) / 255f)
                c.r = i;
            if (i >= (orcChargeUpColor.g * 255f) / 255f)
                c.g = i;
            if (i >= (orcChargeUpColor.b * 255f) / 255f)
                c.b = i;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(chargeUpDeltaTime);
        }
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;

        for (int i = 0; i < 3; ++i)
        {
            animator.SetBool("Orc Attacking", false);
            animator.SetBool("Orc Preparing", true);
            Vector2 playerToBossDirection = transform.position - playerPos; // Look at the player while charging
            float angle = Mathf.Atan2(playerToBossDirection.y, playerToBossDirection.x) * Mathf.Rad2Deg;  
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

            Vector2 hitboxPosition = Vector2.Lerp(transform.position, playerPos, 0.5f);
            float hitboxMagnitude = orcAttackHitboxMagnitude; //Vector2.Distance(transform.position, hitboxPosition);
            Vector2 hitboxDirection = -playerToBossDirection.normalized;


            Vector2 hitboxPosition1 = new Vector2(transform.position.x, transform.position.y) + hitboxDirection * hitboxMagnitude;
            GameObject hitbox = Instantiate(orcAttackHitboxPrefab, hitboxPosition1, Quaternion.Euler(0, 0, angle+90f));
            hitbox.transform.parent = gameObject.transform;
            hitbox.GetComponent<BoxCollider2D>().enabled = false;
            hitbox.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.25f); // change opacity to 25%

            Vector2 hitboxDirection2 = new Vector2(hitboxDirection.x * Mathf.Cos(orcAttackHitboxAngle * Mathf.Deg2Rad) - hitboxDirection.y * Mathf.Sin(orcAttackHitboxAngle * Mathf.Deg2Rad), hitboxDirection.x * Mathf.Sin(orcAttackHitboxAngle * Mathf.Deg2Rad) + hitboxDirection.y * Mathf.Cos(orcAttackHitboxAngle * Mathf.Deg2Rad));
            Vector2 hitboxPosition2 = new Vector2(transform.position.x, transform.position.y) + hitboxDirection2 * hitboxMagnitude;
            GameObject hitbox2 = Instantiate(orcAttackHitboxPrefab, hitboxPosition2, Quaternion.Euler(0, 0, angle+90f+orcAttackHitboxTilt));
            hitbox2.transform.parent = gameObject.transform;
            hitbox2.GetComponent<BoxCollider2D>().enabled = false;
            hitbox2.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.25f); // change opacity to 25%

            Vector2 hitboxDirection3 = new Vector2(hitboxDirection.x * Mathf.Cos(-orcAttackHitboxAngle * Mathf.Deg2Rad) - hitboxDirection.y * Mathf.Sin(-orcAttackHitboxAngle * Mathf.Deg2Rad), hitboxDirection.x * Mathf.Sin(-orcAttackHitboxAngle * Mathf.Deg2Rad) + hitboxDirection.y * Mathf.Cos(-orcAttackHitboxAngle * Mathf.Deg2Rad));
            Vector2 hitboxPosition3 = new Vector2(transform.position.x, transform.position.y) + hitboxDirection3 * hitboxMagnitude;
            GameObject hitbox3 = Instantiate(orcAttackHitboxPrefab, hitboxPosition3, Quaternion.Euler(0, 0, angle+90f-orcAttackHitboxTilt));
            hitbox3.transform.parent = gameObject.transform;
            hitbox3.GetComponent<BoxCollider2D>().enabled = false;
            hitbox3.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.25f); // change opacity to 25%

            yield return new WaitForSeconds(orcAttackChargeSeconds);

            animator.SetBool("Orc Preparing", false);
            animator.SetBool("Orc Attacking", true);
        
            hitbox.GetComponent<BoxCollider2D>().enabled = true;
            hitbox.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); // change opacity to 0%
            
            hitbox2.GetComponent<BoxCollider2D>().enabled = true;
            hitbox2.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); // change opacity to 0%

            hitbox3.GetComponent<BoxCollider2D>().enabled = true;
            hitbox3.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); // change opacity to 0%

            yield return new WaitForSeconds(orcAttackAnimationTime);

            animator.SetBool("Orc Attacking", false);
            animator.SetBool("Forced Idle", true);

            Destroy(hitbox);
            Destroy(hitbox2);
            Destroy(hitbox3);
        }
        
        yield return new WaitForSeconds(forcedIdleAfterAttackingSeconds);

        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        isAttacking = false;
    }


    private IEnumerator TreeAttack()
    {
        isAttacking = true;

        animator.SetBool("Forced Idle", false);
        animator.SetBool("Tree Preparing", true);              

        // GetComponent<SpriteRenderer>().color = orange;
        // yield return new WaitForSeconds(chargeUpDurationSeconds);
        for (float i = 1f; i >= 0; i -= chargeUpDeltaTime)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            if (i >= (treeChargeUpColor.r * 255f) / 255f)
                c.r = i;
            if (i >= (treeChargeUpColor.g * 255f) / 255f)
                c.g = i;
            if (i >= (treeChargeUpColor.b * 255f) / 255f)
                c.b = i;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(chargeUpDeltaTime);
        }
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;

        Debug.Log("Tree attacking state!");

        animator.SetBool("Tree Preparing", false);
        animator.SetBool("Tree Attacking", true);

        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.drag = 0f;
        rb.AddForce((playerPos - transform.position).normalized * treeAttackMovementSpeed, ForceMode2D.Force);     

        yield return new WaitForSeconds(treeAttackMovingSeconds);

        rb.drag = treeAttackDragForceAfterMovement;
        animator.SetBool("Tree Attacking", false);
        animator.SetBool("Forced Idle", true);
        Debug.Log("Forced idle state");
    
        yield return new WaitForSeconds(forcedIdleAfterAttackingSeconds);

        Debug.Log("EXIT attack coroutine");
        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        isAttacking = false;
    }




    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerManager>().TakeDamage(GetComponent<Enemy>().damage);
        }
    }
}
