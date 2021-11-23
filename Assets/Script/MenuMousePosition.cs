using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMousePosition : MonoBehaviour
{
    [SerializeField] public Vector2 MousePos;
    [SerializeField] public GameObject HackChoose;
    [SerializeField] public GameObject BubbleChoose;
    [SerializeField] public GameObject Bubble;
    [SerializeField] public GameObject Hack;
   
    void Start()
    {
        
    }
    
    void Update()
    {
        mouseposcheck();
    }

    bool mouseisleft()
    {
        return (MousePos.x < 0.5f);
    }
    public void mouseposcheck()
    {
        Vector2 mousepos = Camera.main.ScreenToViewportPoint( Input.mousePosition );
        MousePos = mousepos;
        if (mouseisleft() == true)
        {
            Bubble.SetActive(true);
            Hack.SetActive(false);
            HackChoose.SetActive(true);
            BubbleChoose.SetActive(false);
        }
        else
        {
            Bubble.SetActive(false);
            Hack.SetActive(true);
            HackChoose.SetActive(false);
            BubbleChoose.SetActive(true);
        }
        
    }

}
