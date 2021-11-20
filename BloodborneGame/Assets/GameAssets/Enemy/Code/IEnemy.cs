using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Setup(EnemySO enemySO);
    void Attack(Action attackFinished);
    void Damage(int amount, Action<int> damageFinished);
    EnemyEffect GetEffect();
    EnemySO GetEnemySO();
}
