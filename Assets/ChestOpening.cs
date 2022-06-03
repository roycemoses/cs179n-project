using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    public GameObject player;
    public float openRange = 2;
    public int numCoins = 5;
    public AudioSource chestOpeningSound;
    private Animator animator;
    private bool chestOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("playerNearChest", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!chestOpened)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= openRange)
            {
                animator.SetBool("playerNearChest", true);
                chestOpeningSound.Play();
                player.GetComponent<PlayerManager>().player.coins += numCoins;
                chestOpened = true;
            }
        }
    }
}
