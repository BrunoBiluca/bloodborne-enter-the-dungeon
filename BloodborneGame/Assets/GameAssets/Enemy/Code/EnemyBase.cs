using Assets.UnityFoundation.Systems.HealthSystem;
using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public HealthSystem HealthSystem { get; private set; }

    public void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
    }

    public abstract void Setup(EnemySO enemySO);
    public abstract void Attack(Action<int> attackFinished);
    public abstract void Damage(int amount, Action<int> damageFinished);
    public abstract EnemyEffect GetEffect();
    public abstract EnemySO GetEnemySO();
}
