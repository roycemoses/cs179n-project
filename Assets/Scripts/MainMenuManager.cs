using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string newGameScene;
    public string loadGameScene;

    public AudioSource takeDamage;

    public AudioSource death;

    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        player.currHealth = player.baseHealth;
        player.equipHealth = player.baseHealth;
        player.coins = 0;
        player.takeDamageSound = takeDamage;
        player.deathSound = death;
        player.isDead = false;
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(loadGameScene);
    }
}
