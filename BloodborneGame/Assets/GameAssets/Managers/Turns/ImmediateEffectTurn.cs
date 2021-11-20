using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

internal class ImmediateEffectTurn : ITurn
{
    private IEnemy enemy;
    private List<Hunter> hunters;

    public ImmediateEffectTurn(IEnemy enemy, List<Hunter> hunters)
    {
        this.enemy = enemy;
        this.hunters = hunters;
    }

    public void Execute()
    {
        foreach(var hunter in hunters)
        {
            hunter.CurrentCard.Some(currentCard => {
                if(!(currentCard.effect is IImmediateEffect))
                    return;

                if(currentCard.effect is DamageImmediateEffect)
                {
                    ResolveDamageImmediateEffect(currentCard.damage, hunter, currentCard.effect);
                }

                if(currentCard.effect is FullyHealImmediateEffect)
                {
                    ResolveFullyHealImmediateEffect(currentCard.effect);
                }

                hunter.DiscartCard();
            });
        }
    }

    private void ResolveFullyHealImmediateEffect(CardEffect effect)
    {
        ((FullyHealImmediateEffect)effect).Setup(hunters).Handle();
    }

    private void ResolveDamageImmediateEffect(
        int damage, Hunter hunter, CardEffect effect
    )
    {
        ((DamageImmediateEffect)effect).Setup(damage, hunter).Handle();
    }
}