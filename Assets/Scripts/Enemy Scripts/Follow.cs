using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Follow : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public Vector2 initialPos;// get too far from this then disable/enable.
    public float returnDistance;

    private Transform target;//Player
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        initialPos = gameObject.transform.position;
    }
    
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(initialPos, gameObject.transform.position) >= returnDistance)
        {
            gameObject.GetComponent<Patrol2>().enabled = true;
            gameObject.GetComponent<Follow>().enabled = false;
        }

    }
        
}

    
   