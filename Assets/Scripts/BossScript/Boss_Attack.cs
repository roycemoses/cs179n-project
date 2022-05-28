using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{

    private Animator animator;
    public int damage;
    // Start is called before the first frame update


    //parameter passed in is the object this Boss object is colliding with
    void OnTriggerEnter2D(Collider2D col)
    {
        //check if the object you are colliding with has tag player
       if (col.CompareTag("Player"))
        {
            
            Debug.Log("player and boss colliding");
            //have player take damage
            col.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
            GetComponent<Boss_Run>().fast = false;
            animator.SetBool("isMoving", false);
        }
    }
}
