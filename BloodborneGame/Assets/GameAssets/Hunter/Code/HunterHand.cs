using Assets.UnityFoundation.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HunterCardOnHand
{
    public bool Selected { get; set; }
    public HunterCardSO HunterCard { get; set; }
}

public class HunterHand : MonoBehaviour
{
    private Hunter hunter;

    private List<HunterCardOnHand> availableCards;

    private Button handButton;
    private TextMeshProUGUI cardsCountText;
    private Transform hunterSelection;

    public HunterCardSO huntersDreamCard;

    public bool CanGoToHuntersDream { get; set; }

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

        CanGoToHuntersDream = true;
        huntersDreamCard = allCards.huntersDreamCard;

        handButton = transform.FindComponent<Button>("hunter_canvas.hand_button");
        handButton
            .onClick
            .AddListener(() => HunterCardsUI.Instance.Show(this, this.hunter));

        cardsCountText = handButton.transform
            .FindComponent<TextMeshProUGUI>("card_number.text");
        cardsCountText.text = availableCards.Count.ToString();

        hunterSelection = transform.Find("hunter_selection");
        hunterSelection.gameObject.SetActive(false);

        return this;
    }

    public void EnabledCardSelection()
    {
        availableCards.ForEach(c => c.Selected = false);
        handButton.interactable = true;
        hunterSelection.gameObject.SetActive(true);
    }

    public void DisabledCardSelection()
    {
        handButton.interactable = false;
        hunterSelection.gameObject.SetActive(false);
    }

    public void AddCard(HunterCardSO card)
    {
        availableCards.Add(new HunterCardOnHand() {
            Selected = false,
            HunterCard = card
        });

        cardsCountText.text = availableCards.Count.ToString();
    }

    public void DiscartSelectedCard()
    {
        var cardDiscarted = availableCards.FindIndex((card) => card.Selected);

        if(cardDiscarted != -1)
            availableCards.RemoveAt(cardDiscarted);

        cardsCountText.text = availableCards.Count.ToString();
    }

    public void DiscartCard(HunterCardSO currentCard)
    {
        var cardDiscarted = availableCards.FindIndex(
            (card) => card.HunterCard.Equals(currentCard)
        );
        availableCards.RemoveAt(cardDiscarted);

        cardsCountText.text = availableCards.Count.ToString();
    }

    public void SetSelectedCard(int cardIndex)
    {
        availableCards.ForEach(card => card.Selected = false);
        availableCards[cardIndex].Selected = true;
    }
}
