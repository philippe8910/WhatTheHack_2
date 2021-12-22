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

        if(player.GetName() == evnt.Message)
        {
            player.PlayerOnAttack();
        }
    }
}
