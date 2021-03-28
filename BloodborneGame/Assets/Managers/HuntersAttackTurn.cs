using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HuntersAttackTurn : ITurn {

    private IEnemy enemy;
    private List<Hunter> hunters;

    public HuntersAttackTurn(IEnemy enemy, List<Hunter> hunters) {
        this.enemy = enemy;
        this.hunters = hunters;
    }

    public void Execute() {
        Debug.Log("Execute - Hunters Attack Turn");

        foreach(var hunter in hunters) {

            var card = hunter.CurrentCard.OrElse(null);
            if(card == null) continue;

            Debug.Log($"Using {card.cardName}");
            enemy.Damage(
                card.damage,
                (damage) => { 
                    Debug.Log("Damage Finished");

                    Debug.Log("Start - Adding Echoes to Hunter's stock");

                    hunter.AddEchoes(damage);

                    Debug.Log("End - Adding Echoes to Hunter's stock");

                    Debug.Log("Start - Discart card");
                    Debug.Log("End - Discart card");
                }
            );
        }

        Debug.Log("Finish - Hunters Attack Turn");
    }

}