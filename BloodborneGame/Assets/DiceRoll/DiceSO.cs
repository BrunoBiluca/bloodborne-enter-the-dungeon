using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DiceType {
    green,
    yellow,
    red
}

[CreateAssetMenu(menuName = "ScriptableObjects/Dice")]
public class DiceSO : ScriptableObject {
    public DiceType diceType;
    public List<DiceSideSO> sides;
    public int SidesCount { get { return sides.Count; } }
    public Sprite sprite;
}
