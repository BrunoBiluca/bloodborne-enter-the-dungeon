using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscartStackDemoManager : MonoBehaviour {

    [SerializeField] private Button playerChooseCard;
    [SerializeField] private Button discartChosenCard;

    [SerializeField] private Hunter hunter;

    void Start() {
        playerChooseCard.onClick.AddListener(() => {
            hunter.ChooseCard(HunterCardDeck.Instance.GetRandomCard());
        });

        discartChosenCard.onClick.AddListener(() => {
            hunter.DiscartCard();
        });
    }

}
