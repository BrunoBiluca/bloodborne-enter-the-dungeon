using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollManager : MonoBehaviour {
    public static DiceRollManager Instance { get; private set; }

    public void Awake() {
        Instance = this;
    }

    public void Roll(DiceSO dice, Action<DiceSideSO> callback) {
        var selectedIndex = UnityEngine.Random.Range(0, dice.SidesCount);

        var selectedSide = dice.sides[selectedIndex];

        StartCoroutine(CallDiceRollAnimation(dice, selectedSide, callback));
    }

    private IEnumerator CallDiceRollAnimation(
        DiceSO dice, DiceSideSO selectedSide, Action<DiceSideSO> callback
    ) {
        yield return DiceRollUI.Instance.Roll(dice, selectedSide);
        callback(selectedSide);
    }
}
