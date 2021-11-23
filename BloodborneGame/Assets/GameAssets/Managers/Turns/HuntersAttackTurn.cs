using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;

public class HuntersAttackTurn : ITurn
{

    private readonly Optional<EnemyBase> enemy;
    private readonly List<Hunter> hunters;

    public HuntersAttackTurn(Optional<EnemyBase> enemy, List<Hunter> hunters)
    {
        this.enemy = enemy;
        this.hunters = hunters;
    }

    public void Execute()
    {
        foreach(var hunter in hunters)
        {
            if(!hunter.CurrentCard.IsPresentAndGet(out HunterCardSO card))
                continue;

            enemy.Some(e => {
                e.Damage(
                    card.damage,
                    (damageDealt) => hunter.AddEchoes(damageDealt)
                );
            });

            hunter.DiscartCard();
        }
    }

    public bool IsTurnFinished()
    {
        return true;
    }
}