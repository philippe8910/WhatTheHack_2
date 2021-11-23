using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class NetworkCallBack : GlobalEventListener
{
    [SerializeField] private GameObject CubePrefab;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        BoltNetwork.Instantiate(CubePrefab , Vector3.zero,  Quaternion.identity);
    }
}
