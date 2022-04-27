//Base Enemy class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//perhaps abstract
{
    public int damage = 0;
    public int xp = 0;
    public int gold = 0;
    public int maxHealth = 0;
    public int currHealth = 0;
    public HealthBar healthbar;
    public GameObject deathEffect;

    public void Start()
    {
        currHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
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
        healthbar.SetHealth(currHealth);
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
