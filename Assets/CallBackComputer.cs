using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CallBackComputer : GlobalEventListener
{
    [SerializeField] Computers _computers;

    private void Start()
    {
        _computers = GetComponent<Computers>();
    }
    public override void OnEvent(RepairComputer evnt)
    {
        if (evnt.Name == gameObject.name)
        {
            Debug.Log( evnt.Name+":" +evnt.FixedValue);

            _computers.FixValue = evnt.FixedValue;
        }
        
    }
}
