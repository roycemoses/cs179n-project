using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckTile : MonoBehaviour
{

    public Transform player;
    Vector3 playerPos;
    Vector3Int location;
    public Tilemap tiles;
    public AudioSource grassSound;
    public AudioSource dirtSound;
    public bool isPlayingGrassSound;
    public bool isPlayingDirtSound;

    // Update is called once per frame
    void Update()
    {  
        playerPos = player.position;
        location = tiles.WorldToCell(playerPos);
        if (tiles.GetTile(location) == null)
        {
            dirtSound.Stop();
            grassSound.Stop();  
        }
        else
        {
            if (tiles.GetTile(location).name == "Rural Village Terrain32_215")
            {
                // Debug.Log("On Dirt");
                if (player.GetComponent<PlayerMovement>().isMoving && !isPlayingDirtSound)
                {
                    dirtSound.Play();
                    isPlayingDirtSound = true;
                }
                if (!player.GetComponent<PlayerMovement>().isMoving)
                {
                    dirtSound.Pause();
                    isPlayingDirtSound = false;
                }
            }
            else
            {
                dirtSound.Pause();
                isPlayingDirtSound = false;
            }


            if (tiles.GetTile(location).name == "Rural Village Terrain32_5")
            {
                // Debug.Log("On Grass");
                if (player.GetComponent<PlayerMovement>().isMoving && !isPlayingGrassSound)
                {
                    grassSound.Play();
                    isPlayingGrassSound = true;
                }
                if (!player.GetComponent<PlayerMovement>().isMoving)
                {
                    grassSound.Pause();
                    isPlayingGrassSound = false;
                }
            }
            else
            {
                grassSound.Pause();
                isPlayingGrassSound = false;
            }
        }

        


    }
}
