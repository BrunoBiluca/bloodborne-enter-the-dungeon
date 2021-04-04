using Assets.UnityFoundation.GameManagers;
using Assets.UnityFoundation.HealthSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : Singleton<EnemyAttackHandler> {

    public void Handle(DiceSO dice, Action<int> attackFinished) {
        RollDice(
            dice,
            0, 
            (totalDamage) => {
            if(BaseGameManager.Instance.DebugMode) {
                Debug.Log($"Total damage: {totalDamage}");
            }

            var hunters = GameObject.FindGameObjectsWithTag(Tags.hunter);
            foreach(var hunter in hunters) {
                hunter.GetComponent<HealthSystem>().Damage(totalDamage);
            }

            attackFinished(totalDamage);
        });
    }

    private void RollDice(DiceSO dice, int damageAccumulator, Action<int> diceRolled) {
        StartCoroutine(DiceRollManager.Instance.RollWithAnimation(
            dice,
            (selectedSide) => {
                damageAccumulator += selectedSide.value;

                if(BaseGameManager.Instance.DebugMode) {
                    Debug.Log($"Damage: {selectedSide.value}");
                }

                if(selectedSide.plus) RollDice(dice, damageAccumulator, diceRolled);
                else diceRolled(damageAccumulator);
            }
        ));
    }

}
