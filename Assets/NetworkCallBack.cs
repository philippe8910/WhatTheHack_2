using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using UdpKit.Platform.Photon;
using UnityEngine;

public class NetworkCallBack : GlobalEventListener
{
    [SerializeField] private GameObject Hack , Bouble;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {

        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(Hack , Vector3.zero,  Quaternion.identity);
            Debug.Log("IsServer!!!!!!!!!!!!!!!!");
        }
        else
        {
            BoltNetwork.Instantiate(Bouble , Vector3.zero,  Quaternion.identity);
            Debug.Log("IsNotServer!!!!!!!!!!!!!!!!");
        }
        
        
       
    }
}
