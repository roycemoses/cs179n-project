using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_PickUp : MonoBehaviour
{
    //public Transform Player;

   // Player player;

   public Player hero;
   public int armor_stat;

 // Use this for initialization
 private void Start () {
     print($"Before: {hero.equipHealth}");//debug
 }
 
 // Update is called once per frame
 private void Update () {
     //OnCollisionEnter(Player);
     
 }
  private void OnTriggerEnter2D(Collider2D other)
 {
                    hero.equipHealth = hero.equipHealth + armor_stat;//health is initially 100
                    hero.currHealth = hero.currHealth + armor_stat;
                    hero.healthBar.SetMaxHealth(hero.equipHealth);
                    hero.healthBar.SetHealth(hero.currHealth);
                    hero.dam_red = 2;
                    print($"After: {hero.equipHealth}");//debug
                    Destroy(this.gameObject);
                    //or gameObject.SetActive(false);
           
 }
}
