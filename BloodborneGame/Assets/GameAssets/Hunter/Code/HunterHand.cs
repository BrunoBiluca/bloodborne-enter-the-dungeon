using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterCardOnHand
{
    public bool Selected { get; set; }
    public HunterCardSO HunterCard { get; set; }
}

public class HunterHand : MonoBehaviour
{
    private Hunter hunter;

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

    public HunterHand Setup(Hunter hunter)
    {
        this.hunter = hunter;
        // TODO: remover esse código quando for implementar a inicialização das cartas
        var allCards = Resources.Load<HunterCardListSO>("hunter_card_list_so");

        availableCards = new List<HunterCardOnHand>();
        for(int i = 0; i < 5; i++)
        {
            var randomCard = Random.Range(0, allCards.cards.Length);
            availableCards.Add(new HunterCardOnHand() {
                Selected = false,
                HunterCard = allCards.cards[randomCard]
            });
        }

        transform
            .Find("show_hunter_hand_button")
            .GetComponent<Button>()
            .onClick
            .AddListener(() => {
                HunterCardsUI.Instance.Show(this, this.hunter);
            });

        return this;
    }

    public void AddCard(HunterCardSO card)
    {
        availableCards.Add(new HunterCardOnHand() {
            Selected = false,
            HunterCard = card
        });
    }

    public void DiscartCard(HunterCardSO currentCard)
    {
        var cardDiscarted = availableCards.FindIndex(
            (card) => card.HunterCard.Equals(currentCard)
        );
        availableCards.RemoveAt(cardDiscarted);
    }

    public void SetSelectedCard(int cardIndex)
    {
        availableCards.ForEach(card => card.Selected = false);
        availableCards[cardIndex].Selected = true;
    }
}
