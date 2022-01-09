using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceSO diceConfig;

    private List<DiceSideHolder> diceSides;

    private void Awake() {

        diceSides = new List<DiceSideHolder>();
        foreach (Transform diceSide in transform)
        {
            diceSides.Add(diceSide.GetComponent<DiceSideHolder>());
        }
    }

    public Dice Setup(DiceSO diceConfig){
        this.diceConfig = diceConfig;

        for (int i = 0; i < diceConfig.SidesCount; i++)
        {
            diceSides[i].Setup(diceConfig.sides[i]); 
        }

        return this;
    }

}
