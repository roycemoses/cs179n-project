using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolling : MonoBehaviour
{

    public speed;
    private float waitTime;
    public float startWaitTime;

    public transform[] movespots;
    private int randomSpot;


    // Start is called before the first frame update
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, 
        speed * Time.delta);

        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;

            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
