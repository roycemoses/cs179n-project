using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            // Debug.Log(collision.collider);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                int damage = GameObject.Find("Player").GetComponent<Player>().damage;
                collision.gameObject.GetComponent<Villain>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}