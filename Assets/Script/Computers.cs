using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using UnityEngine.UI;

public class Computers : EntityBehaviour<IComputer>
{
    [SerializeField] private float FixValueMax;

    [SerializeField] private float FixValue;

    [SerializeField] private bool IsLock;

    [SerializeField] private Slider FixValueSlider;


    public override void Attached()
    {
        FixValueSlider.maxValue = FixValueMax;
        state.RepairValue = FixValue;
        state.AddCallback("RepairValue" , RepairValueCallBack);
    }

    private void RepairValueCallBack()
    {
        FixValue = state.RepairValue;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRepiaringComputer()
    {
        Debug.Log("StartRepiar");

        StartCoroutine(StartRepiarComputer());
    }

    public void StopRepiaringComputer()
    {
        Debug.Log("StopRepiar");

        StopAllCoroutines();
    }

    private IEnumerator StartRepiarComputer()
    {
        
        if (FixValue < FixValueMax)
        {
            FixValue += 0.1f;
            yield return new WaitForSeconds(0.1f);
            FixValueSlider.value = FixValue;
            StartCoroutine(StartRepiarComputer());
        }
        else
        {
            yield break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }



}
