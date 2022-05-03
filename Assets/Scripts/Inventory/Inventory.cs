using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private List<Item> itemList;

    public Inventory() 
    {
        itemList = new List<Item>();

        // AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1});
        // Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void DisplayItems()
    {
        Debug.Log("itemList.Count: " + itemList.Count);
        // Debug.Log("itemList[0].itemType: " + itemList[0].itemType);
        for (int i = 0; i < itemList.Count; ++i)
        {
            Debug.Log("itemList[i].itemType: " + itemList[i].itemType);
        }
        
    }

}
