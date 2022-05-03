using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    float nextAttackTime = 0f;
    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Time.time >= nextAttackTime && inRange)
        {
            gameObject.GetComponentInChildren<EnemyShoot>().Shoot();
            nextAttackTime = Time.time + 1f / GetComponent<Enemy>().attackRate;
        }
    }
}
