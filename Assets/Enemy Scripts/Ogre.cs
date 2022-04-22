//Example of a child enemy class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{

    public override int health{get;} = 5;
    public override int damage{get;} = 7;
    public override int xp{get;} = 32;
    public override int gold{get;} = 100;
    
    private GameObject enemy;
    private GameObject player;
    private float distance;
    public Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        /*
        //debugs
        print(this.health);
        print(this.damage);
        print(this.xp);
        print(this.gold);
        */
        enemy = GameObject.Find("Ogre");
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
            other.attachedRigidbody.useGravity = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        /*
        distance = Vector2.Distance(enemy.transform.position, player.transform.position);
        print(this.distance);

        if(distance <= 4f)
        {
            Vector2 velocity = new Vector2((transform.position.x - player.transform.position.x) * 2f, (transform.position.y - player.transform.position.y) * 2f);
             GetComponent<Rigidbody2D>().velocity = -velocity;
        }

        */
    }
    
}
