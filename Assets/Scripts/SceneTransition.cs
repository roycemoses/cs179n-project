using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public string sceneToLoad;
    public float transtionTime = 1;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerStorage.initialValue = playerPosition;

            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1));
        }
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
