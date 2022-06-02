//Base Enemy class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//perhaps abstract
{
    public bool isTakingDamage = false;
    public float invicibilityDurationSeconds = 1;
    public float invincibilityDeltaTime = 0.2f;
    public int damage = 0;
    public int xp = 0;
    public int gold = 0;
    public int maxHealth = 0;
    public int currHealth = 0;
    public float attackRange = 0f;
    public float attackRate = 0f;
    public EnemyHealthBar healthbar;
    public GameObject deathEffect;
    public GameObject coin;
    public int coinDelay;
    public bool isDead = false;
    public Color originalColor;
    public bool isUnstoppable = false;

    public AudioSource takeDamageSound;

    void Start()
    {
        currHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        originalColor = GetComponent<SpriteRenderer>().color;
    }
    // Update is called once per frame
    void Update()
    {
        //We will be testing with the space key
        if (Input.GetKeyDown(KeyCode.Alpha5))
            TakeDamage(20);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (isTakingDamage) return;
        takeDamageSound.Play();
        currHealth -= damage;
        healthbar.SetHealth(currHealth);
        if(currHealth <= 0)
        {
            isDead = true;
            DeathEffect();
            //CoinEffect();
            Invoke("CoinEffect", 1);
            this.gameObject.SetActive(false);
        }
        else
            StartCoroutine(StartInvinvibilityFrames());
    }

    private IEnumerator StartInvinvibilityFrames()
    {
        isTakingDamage = true;
        // Color originalColor = GetComponent<SpriteRenderer>().color;
        Color red = Color.red;
        for (float i = 0; i < invicibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            Color currentColor = GetComponent<SpriteRenderer>().color;
            if (currentColor == red)
                GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;
            else
                GetComponent<SpriteRenderer>().color = red;
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        isTakingDamage = false;
        GetComponent<SpriteRenderer>().color = GetComponent<Enemy>().originalColor;      
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
