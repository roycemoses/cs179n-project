using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding

public class Follow : MonoBehaviour
{
    public Transform Player;

    public float speed;
    //public float distance;//distance between player and enemy before being detected
    public Vector2 initialPos; //initial position of enemy when player gets into range
    public float detectRange;
    public float returnRange;//how far from patrol position until return
    bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        //seeker = GetComponent<Seeker>();
        //rb = GetComponent<Rigidbody2D>();
    }
    /*
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p; currentWaypoint = 0;
        }
    }
    */

    // Update is called once per frame
    void Update()
    {

        var distance = Vector2.Distance(transform.position, Player.position);

        if(distance <= detectRange)//this means in range
        {
            //store initial position
            initialPos = transform.position;
            int currPos;

         var MaxDist = 10;
         var MinDist = 5;
         var MoveSpeed = 10f;

            //follow
             //transform.LookAt(Player);
     
     if(Vector2.Distance(transform.position,Player.position) >= MinDist){
     
             float step = speed * Time.deltaTime;
          transform.position = Vector2.MoveTowards(transform.position, Player.position, step);
 
           
           
          
    
    }
            /*
            if(Vector2.Distance(transform.position, Player.position)>1f)
            {//move if distance from target is greater than 1
             transform.Translate(new Vector3(speed* Time.deltaTime,0,0));
             //currPos = transform.position; 
            }

            if(Vector2.Distance(transform.position, initialPos) > returnRange)
            {
                return;
            }
            */

            


        }

    }
}


/*
if (player is within a certain distance)
{
    //do following script

    int initialpos = transform.position;

    //once the enemy is a certain distance away from initial postion then just return;
}
*/