using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackTurn : ITurn {

    public void Execute() {
        var hunters = GameObject.FindGameObjectsWithTag(Tags.hunter);
        var enemies = GameObject.FindGameObjectsWithTag(Tags.enemy);

        foreach(var enemy in enemies) {
            enemy.GetComponent<IEnemy>().Attack(() => {

            });
        }
    }
}
