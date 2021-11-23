using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMousePosition : MonoBehaviour
{
    Vector2 mousepos = Camera.main.ScreenToViewportPoint( Input.mousePosition );

    void Start()
    {
        
    }
    
    void Update()
    {
      Debug.Log(mousepos);
    }

}
