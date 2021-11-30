using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using UdpKit.Platform.Photon;
using UnityEngine;

public class NetworkCallBack : GlobalEventListener
{
    [SerializeField] private GameObject Hack , Bouble , FOV , Camera;
    
    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        GameObject _Player;
        
        if (BoltNetwork.IsServer)
        {
            _Player = BoltNetwork.Instantiate(Hack , Vector3.zero,  Quaternion.identity);
            Debug.Log("IsServer!!!!!!!!!!!!!!!!");
        }
        else
        {
            _Player = BoltNetwork.Instantiate(Bouble , Vector3.zero,  Quaternion.identity);
            Debug.Log("IsNotServer!!!!!!!!!!!!!!!!");
        }
        
        GameObject _FOV = Instantiate(FOV, Vector3.zero, Quaternion.identity);
        //GameObject Cam = Instantiate(Camera, _Player.transform.position, Quaternion.identity);
        
        
        _Player.GetComponent<CharacterView>().fovprefab = _FOV;
        _Player.GetComponent<CharacterView>().fovScript = _FOV.GetComponent<FieldOfView>();

        //Cam.GetComponent<FollowTarget>().objetivo = _Player.transform;

    }
}
