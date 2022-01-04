using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;


public class GlobalEventLis : GlobalEventListener
{
    [SerializeField] PlayerNetwork player;

    private void Start()
    {
        player = GetComponent<PlayerNetwork>();
    }
    public override void OnEvent(PlayerOnAttackEvent evnt)
    {
        Debug.Log(evnt.Message);

        if(evnt.MyProflie.IsOwner && player._PlayerBehaviour != PlayerBehaviour.Hard)
        {
            player.PlayerOnAttack();
            Debug.Log("I am Attacked");
        }

        if (evnt.Message == "TeleportPlayer")
        {
            transform.position = Vector3.zero;
        }
    }
}
