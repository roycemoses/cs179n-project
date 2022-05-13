using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public Player hero;
    public int coinCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //(TextMesh)FindObjectOfType(typeof(TextMesh))
        hero = (Player)FindObjectOfType(typeof(Player));
        //this.hero = GameObject.FindWithTag("Player").transform;
        //coinCounterDisplay = (TextMeshProUGUI)FindObjectOfType(typeof(TextMeshProUGUI));
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
            Destroy(this.gameObject);
        }
    }
}
