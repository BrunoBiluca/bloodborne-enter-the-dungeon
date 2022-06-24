using UnityFoundation.Code;
using System;
using System.Collections;

public class DiceRollManager : Singleton<DiceRollManager>
{
    public DiceSideSO Roll(DiceSO dice)
    {
        var selectedIndex = UnityEngine.Random.Range(0, dice.SidesCount);
        var selectedSide = dice.sides[selectedIndex];
        return selectedSide;
    }

    public IEnumerator RollWithAnimation(DiceSO dice, Action<DiceSideSO> callback)
    {
        var selectedSide = Roll(dice);
        yield return DiceRollUI.Instance.Roll(dice, selectedSide);
        callback(selectedSide);
    }
}
