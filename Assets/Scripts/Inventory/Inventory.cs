using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;


    public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}