using System.Collections.Generic;

public class FullyHealImmediateEffect : CardEffect, IImmediateEffect
{

    public override CardEffectType EffectType()
    {
        return CardEffectType.immediateEffect;
    }

    public FullyHealImmediateEffect Setup(List<Hunter> hunters)
    {
        foreach(var hunter in hunters)
        {
            hunter.HealthSystem.HealFull();
        }

        return this;
    }

    public void Handle()
    {
    }

}
