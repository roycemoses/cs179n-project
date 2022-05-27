using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSFX : MonoBehaviour
{
    public bool isOnFloor;
    public bool isPlayingSound;
    public Transform player;
    public AudioSource floorSound;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "PlayerFootstep")
        {
            isOnFloor = true;
        }
           
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "PlayerFootstep")
        {
            isOnFloor = false;
        }
    }

    void Update()
    {
        if (isOnFloor)
        {
            if (player.GetComponent<PlayerMovement>().isMoving && !isPlayingSound)
            {
                floorSound.Play();
                isPlayingSound = true;
            }
            if (!player.GetComponent<PlayerMovement>().isMoving)
            {
                floorSound.Pause();
                isPlayingSound = false;
            }
        }
        else
        {
            floorSound.Stop();
            isPlayingSound = false;
        }
        
    }
}
