    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;


    public class Patrol2 : MonoBehaviour {

       public float speed;
       private float waitTime;
       public float startWaitTime;
       public float detectRange;
       public Transform Player;

       public Transform[] moveSpots;
       private int randomSpot;


        void Start() 
        {
           waitTime = startWaitTime;
           randomSpot = Random.Range(0, moveSpots.Length);
        }


        


        void Update() 
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            
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

                if(Vector2.Distance(Player.position, transform.position) <= detectRange)//if player gets close to enemy
                {
                    //enable follow and disable patrol2
                    gameObject.GetComponent<Follow>().enabled = true;
                    gameObject.GetComponent<Patrol2>().enabled = false;

                }
            }
        }
    }

