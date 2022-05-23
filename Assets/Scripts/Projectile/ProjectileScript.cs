using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // public GameObject hitEffect;
    public Player player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player"))
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collider.gameObject.CompareTag("Enemy"))
            {
                int damage = GameObject.Find("Player").GetComponent<PlayerManager>().player.damage;
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (!collider.gameObject.CompareTag("Projectile") && !collider.gameObject.CompareTag("Armor"))
                Destroy(gameObject);
        }
    }
}