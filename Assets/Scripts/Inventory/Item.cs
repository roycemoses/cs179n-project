using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType 
    {
        Fists,
        Sword,
        Bow,
        Coin
    }

    public ItemType itemType;
    public int amount;

}
