using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    bool playerInRange = false;
    public int goldCost;
    //public int attack;
    //public int health;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player has " + player.coins + " coins");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerInRange)
        {
            if (player.coins >= goldCost)
            {
                player.coins -= goldCost;
                Debug.Log("Player now has " + player.coins + " coins");
                AddItemToInventory();
                //Destroy(transform.parent.gameObject);
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


 void AddItemToInventory()
    {
        if(playerInventory != null && thisItem != null)
        {
            if(playerInventory.myInventory.Contains(thisItem) && thisItem.unique == false)
            {
                thisItem.amount++;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem); 
                thisItem.amount += 1;
            }

        }
    }
}

