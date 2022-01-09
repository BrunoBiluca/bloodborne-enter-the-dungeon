using Assets.UnityFoundation.Code.TimeUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideChecker : MonoBehaviour
{
    public event Action<DiceSideSO> OnDiceSideChecked;

    private List<Dice> evaluateDices = new List<Dice>();

    private void OnTriggerStay(Collider other) {
        Debug.Log("erwre");
        if(!TryGetComponent(out DiceSideHolder side))
            return;

        var diceVelocity = side.diceOwner.GetComponent<Rigidbody>().velocity;
        if(diceVelocity.magnitude == 0)
            Debug.Log(side.DiceSide.value);
    }
}
