//Base Enemy class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//perhaps abstract
{

    public virtual int health{get;} = 0;
    public virtual int damage{get;} = 0;
    public virtual int xp{get;} = 0;
    public virtual int gold{get;} = 0;
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    */
}
