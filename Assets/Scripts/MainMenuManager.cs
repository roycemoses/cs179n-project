using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string newGameScene;
    public string loadGameScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
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
