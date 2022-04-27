using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int damage = 0;
    public int maxHealth = 100;
    public int currHealth = 0;
    public HealthBar healthBar;
    public bool isDead = false;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Enemies"));
    }

    // Update is called once per frame
    void Update()
    {
        //We will be testing with the space key
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
        
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Respawn();
                isDead = false;
                gameObject.GetComponent<PlayerMovement>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0)
        {
            isDead = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Respawn()
    {
        // this.gameObject.transform.position = this.spawnPoint.position;
        // this.currHealth = this.maxHealth;
        SceneManager.LoadScene(sceneName:"DemoHomeScene");
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
    //         Physics2D.IgnoreCollision(GetComponent<Collider>(), collider);
    // }



    // // public getters
    // int getDamage() { return damage; }
}
