using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{

    public static Vector2 mouseCursorPosition;

    void Awake()
    {  
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPosition;
    }
}
