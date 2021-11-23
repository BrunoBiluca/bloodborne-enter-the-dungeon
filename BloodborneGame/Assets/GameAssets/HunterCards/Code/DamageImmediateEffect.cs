using System.Collections.Generic;
using UnityEngine;

public class DamageImmediateEffect : CardEffect, IImmediateEffect
{
    private EnemyBase enemy;
    public int damage;
    public Hunter hunter;
    public List<Hunter> hunters;

    public DamageImmediateEffect Setup(EnemyBase enemy, int damage, Hunter hunter)
    {
        this.enemy = enemy;
        this.damage = damage;
        this.hunter = hunter;

        return this;
    }

    public void Handle()
    {
        enemy.Damage(
            damage,
            (totalDamage) => {
                hunter.AddEchoes(totalDamage);
            }
        );
    }

    public override CardEffectType EffectType()
    {
        return CardEffectType.immediateEffect;
    }
}
