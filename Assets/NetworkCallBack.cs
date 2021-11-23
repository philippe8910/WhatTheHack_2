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

    [SerializeField] private int x = 0;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        Debug.Log(BoltMatchmaking.CurrentSession.ConnectionsCurrent);
        BoltNetwork.Instantiate(Hack , Vector3.zero,  Quaternion.identity);
    }
}
