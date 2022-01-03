using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuExit : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] public GameObject ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        ExitButton.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("quit");
        ExitButton.SetActive(false);
    }
    
    public void exit()
    {
        Application.Quit();
    }
}
