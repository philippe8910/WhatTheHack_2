using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterView: MonoBehaviour
{

    [SerializeField]
    public LayerMask collisionsLayers;

    public GameObject fovprefab;
    public FieldOfView fovScript;
    
    void Start()
    {
        fovprefab = GameObject.FindGameObjectWithTag("Fov");
        fovScript = fovprefab.GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
       
       fovScript.SetOrigin(transform.position);
    }
    


}