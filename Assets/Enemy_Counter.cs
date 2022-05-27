using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Counter : MonoBehaviour
{

    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        print(enemies.Length);
    }
}
