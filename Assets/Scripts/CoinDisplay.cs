using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public Player hero;
    public int coinCounter = 0;
    public AudioSource pickUpCoin;
    public AudioClip pickUpCoinClip;
    
    // Start is called before the first frame update
    void Start()
    {
        //(TextMesh)FindObjectOfType(typeof(TextMesh))
        // hero = (Player)FindObjectOfType(typeof(Player));
        //this.hero = GameObject.FindWithTag("Player").transform;
        //coinCounterDisplay = (TextMeshProUGUI)FindObjectOfType(typeof(TextMeshProUGUI));
        pickUpCoinClip = pickUpCoin.clip;
    }

    // Update is called once per frame
    void Update()
    {
        //coinCounterDisplay.text = coinCounter.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            hero.coins++;
            coinCounter++;//this needs to be global somehow. Or make the TextMeshPro display Player.coins
            //coinCounterDisplay.text = coinCounter.ToString();
            pickUpCoin.Play();
            transform.position = Vector3.one * 9999f; // move object far away (to seem like the object was destroyed)
            Destroy(this.gameObject, pickUpCoinClip.length); // play the sound and wait until audio clip is finished
        }
    }
}
