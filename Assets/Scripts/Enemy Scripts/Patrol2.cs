using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Patrol2 : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public float startWaitTime;
    public float detectRange;
    public Transform player;

    public List<Transform> moveSpots;
    private int randomSpot;

    private Animator animator;


    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Count);
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.Log("Error: Enemy cannot find player");
        }

        if (moveSpots.Count == 0)
        {
            GameObject topLeft = new GameObject();
            topLeft.transform.position = transform.position + new Vector3(-2, 2, 0);
            GameObject topRight = new GameObject();
            topRight.transform.position = transform.position + new Vector3(2, 2, 0);
            GameObject bottomLeft = new GameObject();
            bottomLeft.transform.position = transform.position + new Vector3(-2, -2, 0);
            GameObject bottomRight = new GameObject();
            bottomRight.transform.position = transform.position + new Vector3(2, -2, 0);
            GameObject center = new GameObject();
            center.transform.position = transform.position;            
            moveSpots.Add(topLeft.transform);
            moveSpots.Add(topRight.transform);
            moveSpots.Add(bottomLeft.transform);
            moveSpots.Add(bottomRight.transform);
            moveSpots.Add(center.transform);
        }

        animator = GetComponent<Animator>();
    }





    void Update()
    {
        Vector2 direction = transform.position - moveSpots[randomSpot].position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            // Debug.Log(angle);

            if (((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180))) // RIGHT
            {
                animator.SetFloat("Xinput", 1.0f);
                animator.SetFloat("Yinput", 0f);
                // Debug.Log("RIGHT");
            }
            else if (((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0))) // LEFT
            {
                animator.SetFloat("Xinput", -1.0f);
                animator.SetFloat("Yinput", 0f);
                // Debug.Log("LEFT");
            }
            else if (angle <= 135 && angle >= 45) // DOWN
            {
                animator.SetFloat("Xinput", 0f);
                animator.SetFloat("Yinput", -1.0f);
                // Debug.Log("DOWN");               
            }
            else if (angle >= -135 && angle <= -45) // UP
            {
                animator.SetFloat("Xinput", 0f);
                animator.SetFloat("Yinput", 1.0f);
                // Debug.Log("UP");   
            }
        }
        else
            animator.SetBool("isMoving", false);

        if (waitTime <= 0)
        {
            randomSpot = Random.Range(0, moveSpots.Count);
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        if (Vector2.Distance(player.position, transform.position) <= detectRange)//if player gets close to enemy
        {
            //enable follow and disable patrol2
            gameObject.GetComponent<Follow>().enabled = true;
            gameObject.GetComponent<Patrol2>().enabled = false;

        }
        else
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
    }
}

