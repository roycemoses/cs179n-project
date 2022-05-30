using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float chargeUpDurationSeconds = 2;
    public bool isAttacking = false;
    float nextAttackTime = 0f;
    bool inRange = false;

    private Animator animator;

    public AudioSource projectileSound;

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
        if (distance > GetComponent<Enemy>().attackRange)
            inRange = false;
        else
            inRange = true;
        if (Time.time >= nextAttackTime && inRange && !isAttacking)
        {
            StartCoroutine(Attack());
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
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Enemy is charging up an attack!");
        yield return new WaitForSeconds(chargeUpDurationSeconds);
        projectileSound.Play();
        gameObject.GetComponentInChildren<EnemyShoot>().Shoot();
        isAttacking = false;
        nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
    }
}
