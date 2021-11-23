using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class NetworkCallBack : GlobalEventListener
{
    [SerializeField] private GameObject Hack , Bouble;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        BoltNetwork.Instantiate(Hack , Vector3.zero,  Quaternion.identity);
    }
}
