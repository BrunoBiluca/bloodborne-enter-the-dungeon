using Assets.UnityFoundation.Code.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HuntersAttackTurn : ITurn
{
    private readonly GameManager gameManager;

    public HuntersAttackTurn(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Execute()
    {
        gameManager.CurrentEnemy.Some(e => {
            e.HealthSystem.OnDied -= (sender, args) => EnemyDiedHandler(e);
            e.HealthSystem.OnDied += (sender, args) => EnemyDiedHandler(e);
        });

        foreach(var hunter in gameManager.GetAliveHunters())
        {
            if(!hunter.CurrentCard.IsPresentAndGet(out HunterCardSO card))
                continue;

            if(card.effect is IAttackEffect)
            {
                gameManager
                    .CurrentEnemy
                    .Some(e => {
                        e.Damage(
                            card.damage,
                            (damageDealt) => hunter.AddEchoes(damageDealt)
                        );
                    });

                hunter.DiscartCard();
            }
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

    public bool IsTurnFinished()
    {
        return true;
    }
}