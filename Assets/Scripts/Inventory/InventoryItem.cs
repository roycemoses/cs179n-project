using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tag
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int amount;
    public bool usable;
    public bool unique;
}
