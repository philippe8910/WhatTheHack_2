using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class OtherEvent : GlobalEventListener
{
    public override void OnEvent(PlayerOtherEvent evnt)
    {
        if (evnt.OwO == "Teleport")
        {
            transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            Debug.Log("Get");
        }
    }
}
