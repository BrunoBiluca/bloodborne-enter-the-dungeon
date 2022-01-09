using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;

internal class ImmediateEffectTurn : ITurn
{
    private GameManager gameManager;

    public ImmediateEffectTurn(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Execute()
    {
        foreach(var hunter in gameManager.GetAliveHunters())
        {
            hunter.CurrentCard.Some(currentCard => {
                if(!(currentCard.effect is IImmediateEffect))
                    return;

                if(currentCard.effect is DamageImmediateEffect)
                {
                    gameManager.CurrentEnemy.Some(e => {
                        e.HealthSystem.OnDied += (sender, args) => EnemyDiedHandler(e);

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

    private void EnemyDiedHandler(EnemyBase e)
    {
        if(e.GetType() != typeof(BossEnemy))
            return;

        foreach(var hunter in gameManager.GetAliveHunters())
        {
            hunter.UpdateCanGoToHuntersDream();
        }
    }

    private void ResolveFullyHealImmediateEffect(CardEffect effect)
    {
        ((FullyHealImmediateEffect)effect).Setup(gameManager.GetAliveHunters()).Handle();
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