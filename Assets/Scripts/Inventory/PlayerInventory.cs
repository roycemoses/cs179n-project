using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptable for if we want them to have a different Inventory when they are fighting
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();
}
