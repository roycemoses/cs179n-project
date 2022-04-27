using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackRate = 10f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            gameObject.GetComponentInChildren<EnemyShoot>().Shoot();
            nextAttackTime = Time.time + 1f / attackRate;
        }        
    }
}
