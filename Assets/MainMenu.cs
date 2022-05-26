using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        // Player should start at the home base when they start the game
        SceneManager.LoadScene("HomeBase");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }
 
}
