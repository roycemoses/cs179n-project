using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject instance;

    private void Awake() {
        if (instance == null) {
            instance = GameObject.Find("Canvas");
            // Debug.Log(instance);
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(GameObject.Find("Canvas"));
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Canvas.ForceUpdateCanvases();
    }

    private void Start() {
        if (player == null) {
            player = Resources.Load<Player>("Player");
            if (player == null) { Debug.Log("Rip"); }
        }
        SetMaxHealth(player.baseHealth);
        SetHealth(player.currHealth);
    }

    private void Update() {
        SetHealth(player.currHealth);  
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //setting health to slider
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
