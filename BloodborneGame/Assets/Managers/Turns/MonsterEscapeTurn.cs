using System.Collections.Generic;
using UnityEngine;

class MonsterEscapeTurn : ITurn {

    public List<Hunter> hunters;

    public MonsterEscapeTurn(List<Hunter> hunters) {
        this.hunters = hunters;
    }

    public void Execute() {
        var enemy = EnemySpawner.Instance.GetEnemy();

        if(enemy.GetEnemySO().isBoss) return;

        if(enemy.GetEffect() is IWhenEscapeEffect effect) {
            if(effect is DamageWhenEscapeEffect ef) {
                ef.Setup(hunters).Handle();
            }

        }

        EnemyDeckManager.Instance.ReturnMonster(enemy.GetEnemySO());
        EnemySpawner.Instance.RemoveEnemy();
    }
}
