using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollManager : MonoBehaviour {
    public static DiceRollManager Instance { get; private set; }

    public void Awake() {
        Instance = this;
    }

    public DiceSideSO Roll(DiceSO dice) {
        var selectedIndex = UnityEngine.Random.Range(0, dice.SidesCount);

        var selectedSide = dice.sides[selectedIndex];
        StartCoroutine(CallDiceRollAnimation(dice, selectedSide));
        return selectedSide;
    }

    private IEnumerator CallDiceRollAnimation(
        DiceSO dice, DiceSideSO selectedSide
    ) {
        yield return DiceRollUI.Instance.Roll(dice, selectedSide);
    }
}
