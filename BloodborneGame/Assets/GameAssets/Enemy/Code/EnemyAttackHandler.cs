using UnityFoundation.Code;
using System;
using UnityEngine;
using UnityFoundation.Code.GameManagers;

public class EnemyAttackHandler : Singleton<EnemyAttackHandler>
{

    public void Handle(DiceSO dice, Action<int> attackFinished)
    {
        RollDice(
            dice,
            0,
            (totalDamage) => {
                if(BaseGameManager.Instance.DebugMode)
                {
                    Debug.Log($"Total damage: {totalDamage}");
                }

                attackFinished(totalDamage);
            });
    }

    private void RollDice(DiceSO dice, int damageAccumulator, Action<int> diceRolled)
    {
        StartCoroutine(DiceRollManager.Instance.RollWithAnimation(
            dice,
            (selectedSide) => {
                damageAccumulator += selectedSide.value;

                if(BaseGameManager.Instance.DebugMode)
                {
                    Debug.Log($"Damage: {selectedSide.value}");
                }

                if(selectedSide.plus) RollDice(dice, damageAccumulator, diceRolled);
                else diceRolled(damageAccumulator);
            }
        ));
    }

}
