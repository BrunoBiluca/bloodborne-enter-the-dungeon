using System.Collections.Generic;
using UnityEngine;

public class RevealMonsterTurn : ITurn
{
    private readonly List<Hunter> hunters;

    public RevealMonsterTurn(List<Hunter> hunters)
    {
        this.hunters = hunters;
    }

    public void Execute()
    {
        var enemy = EnemyDeckManager.Instance.RevealMonster();
        var enemyEffect = enemy.GetEffect();
        if(enemyEffect is IWhenRevealEffect effect)
        {

            if(effect is DamageWhenRevealEffect ef)
            {
                ef.Setup(hunters).Handle();
            }
        }
    }
}