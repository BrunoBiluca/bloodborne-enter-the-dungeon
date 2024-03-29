using DiceRoll;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideChecker : MonoBehaviour
{
    public event Action<DiceSideSO> OnDiceSideChecked;

    private List<Dice> evaluateDices = new List<Dice>();

    private void OnTriggerStay(Collider other) {
        if(!TryGetComponent(out DiceSideHolder side))
            return;

        var diceVelocity = side.diceOwner.GetComponent<Rigidbody>().velocity;
        if(diceVelocity.magnitude == 0)
            Debug.Log(side.DiceSide.value);
    }
}
