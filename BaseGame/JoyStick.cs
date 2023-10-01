using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;
    public Collider2D Area;
    public RawImage[] images;
    


    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);

        foreach (RawImage i in images)
            i.enabled = false;
    }


    void Update()
    {
        Movejoystick();
    }

    private void Movejoystick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved && Area.bounds.Contains(pos))
            {                            
                position = new Vector3(pos.x, pos.y, 0.0f);

                // Position the cube.
                transform.position = position;
            } else if(touch.phase == TouchPhase.Moved)
            {
                Vector3 pos3 = new Vector3(pos.x, pos.y, 0);

                transform.position = pos3 - (pos3 - transform.position);
            }
        }
    }
}
