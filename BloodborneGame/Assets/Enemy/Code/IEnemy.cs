using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {

    public void Setup(EnemySO enemySO);

    public void Attack(Action attackFinished);

    public void Damage(int amount, Action<int> damageFinished);
}
