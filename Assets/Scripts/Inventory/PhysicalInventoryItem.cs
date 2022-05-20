using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;
    // Start is called before the first frame update
    private void Start () { }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !collider.isTrigger){
            AddItemToInventory();
            Destroy(this.gameObject);
        }
        
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
