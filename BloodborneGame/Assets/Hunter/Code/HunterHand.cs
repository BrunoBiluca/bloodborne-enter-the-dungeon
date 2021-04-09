using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterCardOnHand {
    public int Index { get; set; }
    public bool Selected { get; set; }
    public HunterCardSO HunterCard { get; set; }
}

public class HunterHand : MonoBehaviour {

    [SerializeField] private Hunter hunter;

    private List<HunterCardOnHand> availableCards;
    public List<HunterCardOnHand> AvailableCards {
        get {
            return availableCards.FindAll(card => !card.Selected);
        }

        private set {
            availableCards = value;
        }
    }

    public HunterCardOnHand SelectedCard { 
        get { return availableCards.Find(card => card.Selected); }
    }

    private void Awake() {
        // TODO: remover esse código quando for implementar a inicialização das cartas
        var allCards = Resources.Load<HunterCardListSO>("HunterCards");

        availableCards = new List<HunterCardOnHand>();
        for(int i = 0; i < 5; i++) {
            var randomCard = Random.Range(0, allCards.cards.Length);
            availableCards.Add(new HunterCardOnHand() {
                Index = i,
                Selected = false,
                HunterCard = allCards.cards[randomCard]
            });
        }

        transform
            .Find("showHunterCardsButton")
            .GetComponent<Button>()
            .onClick
            .AddListener(() => {
                HunterCardsUI.Instance.Show(this, hunter);
            });
    }

    public void DiscartCard(HunterCardSO currentCard) {
        var cardDiscarted = availableCards.FindIndex(
            (card) => card.HunterCard.Equals(currentCard)
        );
        availableCards.RemoveAt(cardDiscarted);
    }

    public void SetSelectedCard(int cardIndex) {
        availableCards.ForEach(card => card.Selected = false);
        availableCards.Find(card => card.Index == cardIndex).Selected = true;
    }
}
