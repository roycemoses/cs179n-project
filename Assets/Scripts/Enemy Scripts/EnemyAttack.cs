using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Color chargeUpColor;
    public float chargeUpDurationSeconds = 2;
    public float chargeUpDeltaTime = 0.2f;
    public bool isAttacking = false;
    public float nextAttackTime = 0f;
    bool inRange = false;
    public bool exitedAttackEarly = false;
    private Animator animator;

    public AudioSource projectileSound;

    // Start is called before the first frame update
    void Start()
    {
        projectileSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in enemy's attack range
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        float distance = Vector2.Distance(playerPos, transform.position);
        if (distance > GetComponent<Enemy>().attackRange)
            inRange = false;
        else
            inRange = true;
        if (Time.time >= nextAttackTime && inRange && !isAttacking)
        {
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
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Enemy is charging up an attack!");
        // int numFrames = chargeUpDurationSeconds / chargeUpDeltaTime;
        // Color originalColor = GetComponent<SpriteRenderer>().color;
        Color orange = chargeUpColor * 255f;
        // GetComponent<SpriteRenderer>().color = orange;
        // yield return new WaitForSeconds(chargeUpDurationSeconds);
        for (float i = 1f; i >= 0; i -= chargeUpDeltaTime)
        {
            if (GetComponent<Enemy>().isTakingDamage)
            {
                exitedAttackEarly = true;
                GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;
                yield break;
            }
            Color c = GetComponent<SpriteRenderer>().color;
            if (i >= orange.r / 255f)
                c.r = i;
            if (i >= orange.g / 255f)
                c.g = i;
            if (i >= orange.b / 255f)
                c.b = i;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(chargeUpDeltaTime);
        }
        projectileSound.Play();
        gameObject.GetComponentInChildren<EnemyShoot>().Shoot();
        isAttacking = false;
        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;
    }
}
