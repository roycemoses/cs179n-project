using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public HealthBar healthBar;

    private void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        // player.equipHealth = player.baseHealth;
        // player.currHealth = player.baseHealth;
        player.assignHealthBar(healthBar);
        player.healthBar.SetMaxHealth(player.currHealth);
        player.assignCoinCounterDisplay((TextMeshProUGUI)FindObjectOfType(typeof(TextMeshProUGUI)));
        // Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Enemies"));
        player.animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        //We will be testing with the space key
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
        
        if (player.isDead)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Respawn();
                player.currHealth = player.baseHealth;
                player.isDead = false;
                player.animator.SetBool("isDead", false);
                GetComponent<PlayerMovement>().enabled = true;
                // GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        /*int oldval = equipHealth;

        if(equipHealth != oldval)
        {

        healthBar.SetMaxHealth(equipHealth);//if armor is picked up, increase this. put this in update
        }
        */

        player.coinCounterDisplay.text = player.coins.ToString();
    }

    public void TakeDamage(int damage)
    {
        player.currHealth -= damage/player.dam_red;
        player.healthBar.SetHealth(player.currHealth);
        if (player.currHealth <= 0 && !player.isDead)
            DeathEffect();
    }

    public void AddLife(int lifeAdded)
    {
        player.currHealth += lifeAdded;
        player.healthBar.SetHealth(player.currHealth);
    }

    private void DeathEffect()
    {
        player.isDead = true;
        player.animator.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().AddForce(transform.up * 600f);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
        StartCoroutine(DeathRotationCoroutine());
    }

    IEnumerator DeathRotationCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        // GetComponent<Rigidbody2D>().AddForce(-transform.up * 200f);
        GetComponent<Rigidbody2D>().gravityScale = 2.5f;
        float tiltDegree = 0;
        // Debug.Log(tiltAroundZ);
        while (tiltDegree < 90)
        {
            tiltDegree += 3;
            Quaternion target = Quaternion.Euler(0, 0, tiltDegree);
            transform.rotation = target;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Respawn()
    {
        // this.gameObject.transform.position = this.spawnPoint.position;
        // this.currHealth = this.maxHealth;
        SceneManager.LoadScene(sceneName:"Scenes/MainScenes/HomeBase");
    }
}
