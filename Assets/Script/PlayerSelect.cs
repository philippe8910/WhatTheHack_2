using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class PlayerSelect : GlobalEventListener
{
    [SerializeField] private GameObject Hack , Bouble;
    // Start is called before the first frame update
    void Start()
    {
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(Hack , Vector3.zero,  Quaternion.identity);
        }
        else
        {
            BoltNetwork.Instantiate(Bouble , Vector3.zero,  Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

}
