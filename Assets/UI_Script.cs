using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Namespace required for Button objects.
using UnityEngine.EventSystems;

public class UI_Script : MonoBehaviour
{

    public Transform[] Slots;
     float r = 0;  // red component
 float g = 0;  // green component
 float b = 0;  // blue component
 float a = (float)0.1764706;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slots[0].GetComponent<Image>().color = new Color(0,0,0);

            for(int i = 0; i < Slots.Length; i++)
            {
                if(i == 0)
                {
                    continue;
                }
                Slots[i].GetComponent<Image>().color = new Color(0,0,0,a);//reset other buttons
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Slots[1].GetComponent<Image>().color = new Color(0,0,0);//change

            for(int i = 0; i < Slots.Length; i++)
            {
                if(i == 1)//change
                {
                    continue;
                }
                Slots[i].GetComponent<Image>().color = new Color(0,0,0, a);//reset other buttons
            }
        }
        
        
    }
}
