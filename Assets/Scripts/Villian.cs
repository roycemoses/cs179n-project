using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villian : MonoBehaviour
{
    public HealthBar hb;
    public int maxHealth = 100;
    public int currHealth;
    public GameObject deathEffect;
    public GameObject coin;
    public int coinDelay;
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

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        hb.SetHealth(currHealth);
        if(currHealth <= 0)
        {
            DeathEffect();
            //CoinEffect();
            Invoke("CoinEffect", 1);
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
    private void CoinEffect()
    {
        if (coin != null)
        {
            GameObject effect = Instantiate(coin, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
        }
    }
}