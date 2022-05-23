using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Tag 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
//This is just a container of informations for the inventory item
//This is different than item because not evey item is an invetory item
//ScriptableObject so that we can create it from editor
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int amount;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
    public void Use()
    {
        Debug.Log("Using Item");
        thisEvent.Invoke();
    }
    
    public void updateAmount(int decrease)
    {
        //Will only implement taking away still have to implement picking up
        amount -= decrease;
        if(amount < 0)
        {
            amount = 0;
        }
    }
}
