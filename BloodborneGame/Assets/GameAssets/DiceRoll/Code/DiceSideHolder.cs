using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideHolder : MonoBehaviour
{
    public Dice diceOwner {get; private set;}
    public DiceSideSO DiceSide { get; private set; }

    public void Setup(DiceSideSO diceSide)
    {
        this.DiceSide = diceSide;

        diceOwner = transform.parent.GetComponent<Dice>();
    }
}
