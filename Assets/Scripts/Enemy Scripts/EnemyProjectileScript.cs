using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    // public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collision.gameObject.CompareTag("Player"))
            {
                int damage = gameObject.transform.parent.gameObject.GetComponent<Enemy>().damage;
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            }            
            Destroy(gameObject);
        }
    }
}