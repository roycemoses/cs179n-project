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
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Enemies"));
        animator = GetComponent<Animator>();
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
                animator.SetBool("isDead", false);
                GetComponent<PlayerMovement>().enabled = true;
                // GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0 && !isDead)
            DeathEffect();
    }

    private void DeathEffect()
    {
        isDead = true;
        animator.SetBool("isDead", true);
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
        SceneManager.LoadScene(sceneName:"DemoHomeScene");
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
    //         Physics2D.IgnoreCollision(GetComponent<Collider>(), collider);
    // }



    // // public getters
    // int getDamage() { return damage; }
}
