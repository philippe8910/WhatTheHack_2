using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUserName : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text name;
    public void SetPlayerPrefabs()
    {
        PlayerPrefs.SetString("UserName", name.text);

        Debug.Log(name);
    }
}
