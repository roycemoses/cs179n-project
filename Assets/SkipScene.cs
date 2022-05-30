using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutScene = null;
    [SerializeField] private double timeToSkipTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            cutScene.time = timeToSkipTo;
        }
    }
}
