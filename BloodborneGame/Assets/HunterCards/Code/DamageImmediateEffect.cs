using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageImmediateEffect : CardEffect, IImmediateEffect {

    public int damage;
    public Hunter hunter;
    public List<Hunter> hunters;

    public DamageImmediateEffect Setup(int damage, Hunter hunter) {

        this.damage = damage;
        this.hunter = hunter;

        return this;
    }

    public void Handle() {
        var enemies = GameObject.FindGameObjectsWithTag(Tags.enemy);

        foreach(var enemy in enemies) {
            enemy.GetComponent<IEnemy>().Damage(
                damage, 
                (totalDamage) => {
                    hunter.AddEchoes(totalDamage);
                }
            );
        }
    }

    public override CardEffectType EffectType() {
        return CardEffectType.immediateEffect;
    }
}
