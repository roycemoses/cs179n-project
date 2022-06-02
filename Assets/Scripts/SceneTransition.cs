using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public void OnTriggerEnter2D(Collider2D other){
        // player enters scene transition
        if(other.CompareTag("Player") && !other.isTrigger){
            playerStorage.initialValue = playerPosition;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name != "OpenCutScene" && scene.name != "YardCutScene")
                GameObject.Find("Player").GetComponent<PlayerManager>().player.prev_scene = scene.name;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
