using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    //[SerializeField] private PlayerInventory playerInventory;
    //[SerializeField] private InventoryItem thisItem;
    public Player hero;
    public int potion_inc;
    
    // Use this for initialization
    private void Start () {
        // print($"Before: {hero.equipHealth}");//debug
    }
 
    // Update is called once per frame
    private void Update () {
        //OnCollisionEnter(Player);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && (hero.currHealth != hero.equipHealth))
        {
            if (hero.currHealth + potion_inc > hero.equipHealth)
                hero.currHealth = hero.equipHealth;
            else
                hero.currHealth = hero.currHealth + potion_inc;
            hero.healthBar.SetHealth(hero.currHealth);
            //hero.dam_red = 2;
            Destroy(this.gameObject);
        }
    }     
 }