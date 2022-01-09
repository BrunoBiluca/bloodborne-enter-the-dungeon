using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : CardEffect, IAttackEffect
{
    public override CardEffectType EffectType()
    {
        return CardEffectType.attackEffect;   
    }

    public float Evaluate()
    {
        return 123f;
    }
}
