using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscartStackDemoManager : MonoBehaviour
{
    [SerializeField] private Button addCardToHunterHand;
    [SerializeField] private Button playerChooseCard;
    [SerializeField] private Button discartChosenCard;

    [SerializeField] private Hunter hunter;

    void Start()
    {
        addCardToHunterHand.onClick.AddListener(() => {
            hunter.AddCardToHand(HunterCardDeck.Instance.GetRandomCard());
        });

        playerChooseCard.onClick.AddListener(() => {
            hunter.ChooseCard(0);
        });

        discartChosenCard.onClick.AddListener(() => {
            hunter.DiscartCard();
        });
    }

}
