using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    // public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collider.gameObject.CompareTag("Player"))
            {
                int damage = gameObject.transform.parent.gameObject.GetComponent<Enemy>().damage;
                collider.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
            }
            if (!collider.gameObject.CompareTag("Projectile") && !collider.gameObject.CompareTag("PlayerTrigger"))
                Destroy(gameObject);
        }
    }
}