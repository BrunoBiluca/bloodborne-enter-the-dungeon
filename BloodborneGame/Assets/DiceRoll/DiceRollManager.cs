using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollManager : MonoBehaviour {
    public static DiceRollManager Instance { get; private set; }

    public Dice greenDice;

    public void Awake() {
        Instance = this;

        greenDice = new Dice()
            .AddSide(DiceSide.ZERO)
            .AddSide(DiceSide.ZERO)
            .AddSide(DiceSide.ONE)
            .AddSide(DiceSide.ONEPLUS)
            .AddSide(DiceSide.ONEPLUS)
            .AddSide(DiceSide.TWO);
    }

    public DiceSide Roll() {
        return greenDice.Roll();
    }
}
