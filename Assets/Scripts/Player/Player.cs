using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Player", menuName = "DrackAdventures/Player", order = 0)]
public class Player : ScriptableObject 
{
    public string prev_scene;
    public bool hasInstance = false;
    public int damage = 0;
    //public int maxHealth = 100;
    public int baseHealth = 100;
    public int equipHealth;//baseHealth + armor stats
    public int currHealth;
    public HealthBar healthBar;
    public int oldEquip;
    public int dam_red = 1;//damage reduction factor
    public bool isDead = false;
    public Transform spawnPoint;
    public Animator animator;
    public int coins = 0;
    public TextMeshProUGUI coinCounterDisplay;
    public AudioSource takeDamageSound;
    public AudioSource deathSound;

    public void assignHealthBar(HealthBar healthBar)
    {
        this.healthBar = healthBar;
    }

    public void assignCoinCounterDisplay(TextMeshProUGUI display)
    {
        this.coinCounterDisplay = display;
    }
}
