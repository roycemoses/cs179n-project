using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{

    private Animator animator;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //parameter passed in is the object this Boss object is colliding with
    void OnCollisionEnter(Collider2D collider)
    {
        //check if the object you are colliding with has tag player
       if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("player and boss colliding");
            collider.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
