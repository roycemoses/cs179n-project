//Example of a child enemy class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{

    public override int health{get;} = 5;
    public override int damage{get;} = 7;
    public override int xp{get;} = 32;
    public override int gold{get;} = 100;
    
 
    void Start()
    {
  
        
    }

    
    

    // Update is called once per frame
    void Update()
    {
       
    }
    
}