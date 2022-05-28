using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Player player;

    private void Awake() {

    }

    public void PlayGame ()
    {
        // Player should start at the home base when they start the game
        player.currHealth = player.baseHealth;
        player.equipHealth = player.baseHealth;
        player.isDead = false;
        SceneManager.LoadScene("Scenes/DanzScenes/HomeBase");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }
 
}
