using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

internal class ImmediateEffectTurn : ITurn {
    private IEnemy enemy;
    private List<Hunter> hunters;

    public ImmediateEffectTurn(IEnemy enemy, List<Hunter> hunters) {
        this.enemy = enemy;
        this.hunters = hunters;
    }

    public void Execute() {
        foreach(var hunter in hunters) {
            hunter.CurrentCard.Some(currentCard => {
                if(!(currentCard.effect is IImmediateEffect))
                    return;

                if(currentCard.effect is DamageImmediateEffect effect) {
                    effect.Setup(currentCard.damage, hunter)
                          .Handle();
                }
                hunter.DiscartCard();
            });
        }
    }
}