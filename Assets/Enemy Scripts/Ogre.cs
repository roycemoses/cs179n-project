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
    
    // Start is called before the first frame update
    void Start()
    {
        //debugs
        // print(this.health);
        // print(this.damage);
        // print(this.xp);
        // print(this.gold);
    }
    /*

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
