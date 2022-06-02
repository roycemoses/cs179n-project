using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.gameObject.CompareTag("Player"))
        {
            int damage = gameObject.transform.parent.gameObject.GetComponent<Enemy>().damage;
            collider.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }
}
