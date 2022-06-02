using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Namespace required for Button objects.
using UnityEngine.EventSystems;

public class UI_Script : MonoBehaviour
{

    public Transform[] Slots;
    public Button HPButton;
    public Button tier2HPButton;
    public Button tier3HPButton;
    // float r = 0;  // red component
    // float g = 0;  // green component
    // float b = 0;  // blue component
    float a = (float)0.1764706;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slots[0].GetComponent<Image>().color = new Color(0, 0, 0);

            for (int i = 0; i < 1; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                Slots[i].GetComponent<Image>().color = new Color(0, 0, 0, a);//reset other buttons
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Slots[1].GetComponent<Image>().color = new Color(0, 0, 0);//change

            for (int i = 0; i < 1; i++)
            {
                if (i == 1)//change
                {
                    continue;
                }
                Slots[i].GetComponent<Image>().color = new Color(0, 0, 0, a);//reset other buttons
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
             HPButton.onClick.Invoke();
             var go = HPButton.gameObject;
             var ped = new PointerEventData(EventSystem.current);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.pointerEnterHandler);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.submitHandler);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tier2HPButton.onClick.Invoke();
             var go = tier2HPButton.gameObject;
             var ped = new PointerEventData(EventSystem.current);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.pointerEnterHandler);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.submitHandler);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
             tier3HPButton.onClick.Invoke();
             var go = tier3HPButton.gameObject;
             var ped = new PointerEventData(EventSystem.current);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.pointerEnterHandler);
             ExecuteEvents.Execute(go, ped, ExecuteEvents.submitHandler);
            
        }











        if (Input.GetKeyDown("c"))
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].GetComponent<Image>().color = new Color(0, 0, 0, a);//reset other buttons
            }
        }

    }
}
