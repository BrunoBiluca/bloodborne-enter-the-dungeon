using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;

internal class ImmediateEffectTurn : ITurn
{
    private Optional<EnemyBase> enemy;
    private List<Hunter> hunters;

    public ImmediateEffectTurn(Optional<EnemyBase> enemy, List<Hunter> hunters)
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
                    enemy.Some(e => {
                        ResolveDamageImmediateEffect(
                            e,
                            currentCard.damage,
                            hunter,
                            currentCard.effect
                        );
                    });
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
        EnemyBase enemy, int damage, Hunter hunter, CardEffect effect
    )
    {
        ((DamageImmediateEffect)effect).Setup(enemy, damage, hunter).Handle();
    }

    public bool IsTurnFinished()
    {
        return true;
    }
}