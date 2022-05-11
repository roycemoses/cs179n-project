using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerInRange)
        {
            Debug.Log("Player wants to buy the item!");
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        playerInRange = true;
        Debug.Log("Player is in range!");
    }

    private void OnTriggerExit2D(Collider2D other) {
        playerInRange = false;
        Debug.Log("Player left the buy range!");
    }
}
