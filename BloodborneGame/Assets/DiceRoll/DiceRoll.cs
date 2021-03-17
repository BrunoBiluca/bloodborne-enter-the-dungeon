using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceSide : EnumX<DiceSide> {

    public static DiceSide ZERO = new DiceSide(0, "ZERO");
    public static DiceSide ONE = new DiceSide(1, "ONE");
    public static DiceSide ONEPLUS = new DiceSide(2, "ONEPLUS");
    public static DiceSide TWO = new DiceSide(3, "TWO");
    public static DiceSide TWOPLUS = new DiceSide(4, "TWOPLUS");
    public static DiceSide THREE = new DiceSide(5, "THREE");
    public static DiceSide FOUR = new DiceSide(6, "FOUR");

    public DiceSide(int index, string name) : base(index, name) { }
}

public class Dice {

    public List<DiceSide> sides;
    public int SidesNumber { get { return sides.Count; }}

    public Dice() {
        sides = new List<DiceSide>();
    }

    public Dice AddSide(DiceSide side) {
        sides.Add(side);
        return this;
    }

    public DiceSide Roll() {
        var side = Random.Range(0, SidesNumber);
        return sides[side];
    }
}