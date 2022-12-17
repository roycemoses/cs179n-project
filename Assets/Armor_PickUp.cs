using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_PickUp : MonoBehaviour
{
    //public Transform Player;

   // Player player;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;
   public Player hero;
   public int armor_stat;
   public AudioSource armorPickupSound;
   public AudioClip armorPickupSoundClip;

 // Use this for initialization
 private void Start () {
     print($"Before: {hero.equipHealth}");//debug
     armorPickupSoundClip = armorPickupSound.clip;
 }
 
 // Update is called once per frame
 private void Update () {
     //OnCollisionEnter(Player);
     
 }
  private void OnTriggerEnter2D(Collider2D other)
 {
            if(other.tag == "Player")
            {
                    hero.equipHealth = hero.equipHealth + armor_stat;//health is initially 100
                    hero.currHealth = hero.currHealth + armor_stat;
                    hero.healthBar.SetMaxHealth(hero.equipHealth);
                    hero.healthBar.SetHealth(hero.currHealth);
                    hero.dam_red = 2;
                    print($"After: {hero.equipHealth}");//debug
                    armorPickupSound.Play();
                    transform.position = Vector3.one * 9999f; // move object far away (to seem like the object was destroyed)
                    Destroy(this.gameObject, armorPickupSoundClip.length); // wait for the sound before destroying
                    //or gameObject.SetActive(false);
    if (other.gameObject.CompareTag("Player") && !other.isTrigger){
            AddItemToInventory();
            armorPickupSound.Play();
            transform.position = Vector3.one * 9999f; // move object far away (to seem like the object was destroyed)
            Destroy(this.gameObject, armorPickupSoundClip.length);
    }
           
 }
 }
 void AddItemToInventory()
    {
        if(playerInventory != null && thisItem != null)
        {
            if(playerInventory.myInventory.Contains(thisItem))
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