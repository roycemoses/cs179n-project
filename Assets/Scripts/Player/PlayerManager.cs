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
        Debug.Log("AWAKE");
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded: " + scene.name);
        if (player.prev_scene != scene.name)
        {
            if (player.prev_scene == "House")
                player.spawnPoint = GameObject.Find("FromHouseSpawn").transform;
            else if (player.prev_scene == "Neighborhood" || player.prev_scene == "YardCutScene")
            {
                player.spawnPoint = GameObject.Find("FromNeighborhoodSpawn").transform;
            }
            else if (player.prev_scene == "HomeBase")
                player.spawnPoint = GameObject.Find("FromHomeBaseSpawn").transform;
        }
        else
        {
            player.spawnPoint = GameObject.Find("PlayerSpawn").transform;
        }
        // Debug.Log(mode);
    }

    // Start is called before the first frame update
    void Start()
    {
        // player.equipHealth = player.baseHealth;
        // player.currHealth = player.baseHealth;
        player.isInvincible = false;
        player.assignHealthBar(healthBar);
        player.healthBar.SetMaxHealth(player.currHealth);
        player.assignCoinCounterDisplay((TextMeshProUGUI)FindObjectOfType(typeof(TextMeshProUGUI)));
        if (transform != null && player.spawnPoint != null)
            transform.position = player.spawnPoint.position;    
        // if (this == null)
        // {
        //     Debug.Log("assigning spawn to player");
        //     transform.position = player.spawnPoint.position;
        // }
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
                player.currHealth = player.baseHealth;
                player.isDead = false;
                player.animator.SetBool("isDead", false);
                // GetComponent<PlayerMovement>().enabled = true;
                // GetComponent<SpriteRenderer>().enabled = true;
                // GetComponent<BoxCollider2D>().enabled = true;
                Respawn();
            }
        }
        /*int oldval = equipHealth;

        if(equipHealth != oldval)
        {

        healthBar.SetMaxHealth(equipHealth);//if armor is picked up, increase this. put this in update
        }
        */

        if (player.coinCounterDisplay != null)
            player.coinCounterDisplay.text = player.coins.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (player.isInvincible) return;
        player.currHealth -= damage/player.dam_red;
        player.healthBar.SetHealth(player.currHealth);
        if (player.currHealth <= 0 && !player.isDead)
            DeathEffect();
        else
            StartCoroutine(StartInvinvibilityFrames());
    }

    private IEnumerator StartInvinvibilityFrames()
    {
        // Debug.Log("Player turned invincible!");
        player.isInvincible = true;
        Color originalColor = GetComponent<SpriteRenderer>().color;
        Color red = Color.red;
        for (float i = 0; i < player.invicibilityDurationSeconds; i += player.invincibilityDeltaTime)
        {
            Color currentColor = GetComponent<SpriteRenderer>().color;
            if (currentColor == red)
                GetComponent<SpriteRenderer>().color = originalColor;
            else
                GetComponent<SpriteRenderer>().color = red;
            yield return new WaitForSeconds(player.invincibilityDeltaTime);
        }
        player.isInvincible = false;
        // Debug.Log("Player is no longer invincible!");
        GetComponent<SpriteRenderer>().color = originalColor;
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
