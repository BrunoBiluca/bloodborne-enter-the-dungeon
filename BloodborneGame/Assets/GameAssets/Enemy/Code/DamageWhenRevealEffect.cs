using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhenRevealEffect : EnemyEffect, IWhenRevealEffect
{
    private List<Hunter> hunters;

    public DamageWhenRevealEffect Setup(List<Hunter> hunters)
    {
        this.hunters = hunters;

        return this;
    }

    public void Handle()
    {
        foreach(var hunter in hunters)
        {
            EnemyEffectParameter parameter = Parameters.Find(p => p.key == "damage");

            if(parameter == null)
                throw new ArgumentException(
                    "DamageWhenRevealEffect expected a parameter named damage to work properly."
                );

            hunter.HealthSystem.Damage(int.Parse(parameter.value));
        }
    }
}
