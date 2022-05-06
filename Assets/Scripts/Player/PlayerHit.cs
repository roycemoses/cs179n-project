using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Hit " + collider.name);
        if (!collider.gameObject.CompareTag("Player"))
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collider.gameObject.CompareTag("Enemy"))
            {
                int damage = GameObject.Find("Player").GetComponent<Player>().damage;
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }                    
        }
    }
}
