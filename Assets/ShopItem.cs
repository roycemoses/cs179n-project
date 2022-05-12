using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    bool playerInRange = false;
    public int goldCost;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player has " + GameObject.Find("Player").GetComponent<Player>().coins + " coins");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerInRange)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player.coins >= goldCost)
            {
                player.coins -= goldCost;
                Debug.Log("Player now has " + player.coins + " coins");
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        playerInRange = true;
        Debug.Log("Player is in range!");
    }

    private void OnTriggerExit2D(Collider2D other) {
        playerInRange = false;
        Debug.Log("Player left the buy range!");
    }
}
