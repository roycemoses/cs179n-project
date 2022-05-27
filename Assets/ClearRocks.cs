using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRocks : MonoBehaviour
{
    public Enemy_Counter enemy_counter;
    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_counter.enemies.Length == 0)
        {
            //DeathEffect();
            Destroy(this.gameObject);
             
        }
    }

    private void DeathEffect()
    {
        
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        
    }
}
