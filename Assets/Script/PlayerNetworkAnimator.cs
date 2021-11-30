using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class PlayerNetworkAnimator : MonoBehaviour
{
    public  void Attached()
    {
        //state.SetTransforms(state.PlayerAnimatorTransform , transform);
    }

    public void FacingRight()
    {
        transform.rotation = Quaternion.Euler(0,180,0);
    }

    public void FacingLeft()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
