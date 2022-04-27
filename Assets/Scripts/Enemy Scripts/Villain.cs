using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    public HealthBar hb;
    public int maxHealth = 100;
    public int currHealth;
    public GameObject deathEffect;
    void Start()
    {
        currHealth = maxHealth;
        hb.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        //We will be testing with the space key
        if (Input.GetKeyDown(KeyCode.Alpha5))
            TakeDamage(20);
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        hb.SetHealth(currHealth);
        if(currHealth <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }
    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}