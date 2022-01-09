using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntersDreamEffect : CardEffect, IDefenseEffect
{
    public override CardEffectType EffectType()
    {
        return CardEffectType.huntersDreamEffect;
    }

    public float Evaluate(float damage)
    {
        return Mathf.Ceil(damage/ 2f);
    }
}
