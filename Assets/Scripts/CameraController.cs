using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothing;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != player.position){
            Vector3 playerPosition = new Vector3(player.position.x, player.position.y, transform.position.z);    

            playerPosition.x = Mathf.Clamp(playerPosition.x,minPosition.x,maxPosition.x);
            playerPosition.y = Mathf.Clamp(playerPosition.y,minPosition.y,maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
        }
        
        //only change the position of the camera according to the player on the x axis and y axis since its a 2D game

    }
}
