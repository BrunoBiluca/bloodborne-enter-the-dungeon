using System.Collections.Generic;
using UnityEngine;

public class HuntersAttackTurn : ITurn
{

    private IEnemy enemy;
    private List<Hunter> hunters;

    public HuntersAttackTurn(IEnemy enemy, List<Hunter> hunters)
    {
        this.enemy = enemy;
        this.hunters = hunters;
    }

    public void Execute()
    {
        Debug.Log("Execute - Hunters Attack Turn");

        foreach(var hunter in hunters)
        {
            if(!hunter.CurrentCard.IsPresent) continue;

            var card = hunter.CurrentCard.Get();

            Debug.Log($"Using {card.cardName}");
            enemy.Damage(
                card.damage,
                (damageDealt) => {
                    Debug.Log("Damage Finished");

                    Debug.Log("Start - Adding Echoes to Hunter's stock");

                    hunter.AddEchoes(damageDealt);

                    Debug.Log("End - Adding Echoes to Hunter's stock");

                    Debug.Log("Start - Discart card");
                    Debug.Log("End - Discart card");
                }
            );
        }

        Debug.Log("Finish - Hunters Attack Turn");
    }

}