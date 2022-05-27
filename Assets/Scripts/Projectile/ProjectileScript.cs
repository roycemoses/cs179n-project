using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player") && LayerMask.LayerToName(collider.gameObject.layer) != "Floor")
        {
            Debug.Log(collider);
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collider.gameObject.CompareTag("Enemy"))
            {
                int damage = GameObject.Find("Player").GetComponent<Player>().damage;
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (!collider.gameObject.CompareTag("Projectile") && !collider.gameObject.CompareTag("Armor"))
                Destroy(gameObject);
        }
    }
}