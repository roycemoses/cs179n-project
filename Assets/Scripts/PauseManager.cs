using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;

    public AudioSource openPauseMenuSound;
    public AudioSource closePauseMenuSound; 

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("escape"))
        {
            ChangePause();
        }

    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            openPauseMenuSound.Play();
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        else
        {
            closePauseMenuSound.Play();
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
             Cursor.visible = false;
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }


}