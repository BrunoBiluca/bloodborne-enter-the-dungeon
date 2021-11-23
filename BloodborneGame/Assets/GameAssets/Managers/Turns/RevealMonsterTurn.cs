using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;

public class RevealMonsterTurn : ITurn
{
    private readonly GameManager gameManager;
    private readonly List<Hunter> hunters;

    public RevealMonsterTurn(GameManager gameManager, List<Hunter> hunters)
    {
        this.gameManager = gameManager;
        this.hunters = hunters;
    }

    public void Execute()
    {
        if(gameManager.CurrentEnemy.IsPresent)
            return;

        var enemy = EnemyDeckManager.Instance.RevealMonster();
        var enemyEffect = enemy.GetEffect();
        if(enemyEffect is IWhenRevealEffect effect)
        {
            if(effect is DamageWhenRevealEffect ef)
            {
                ef.Setup(hunters).Handle();
            }
        }

        gameManager.CurrentEnemy = Optional<EnemyBase>.Some(enemy);
    }

    public bool IsTurnFinished()
    {
        return true;
    }
}