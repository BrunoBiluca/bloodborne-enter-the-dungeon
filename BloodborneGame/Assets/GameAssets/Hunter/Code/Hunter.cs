using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Systems.HealthSystem;
using System;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private StockSystem stockSystem;
    private Transform hunterCardReferencePoint;
    private Transform hunterHandTransform;
    private HunterHand hunterHand;

    public HunterSO hunterConfig;
    public bool IsDead { get; private set; }
    public Optional<HunterCardSO> CurrentCard { get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    public DiscartStackSystem DiscartStackSystem { get; private set; }
    public int HunterHandCardsCount => hunterHand.AvailableCards.Count;
    public int EchoesCount => stockSystem.Count;

    private void Awake()
    {
        hunterCardReferencePoint = transform.Find("hunter_card_reference_point");
    }

    void Start()
    {
        stockSystem = transform.Find("echoes_stock").GetComponent<StockSystem>();
        DiscartStackSystem = GetComponent<DiscartStackSystem>();

        CurrentCard = Optional<HunterCardSO>.None();

        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.SetDestroyHealthbar(false);
        HealthSystem.SetDestroyOnDied(false);
        HealthSystem.OnDied += (sender, args) => {
            IsDead = true;
            stockSystem.RemoveAll();
            DiscartCard();

            gameObject.SetActive(false);
        };

        hunterHand = GetComponent<HunterHand>().Setup(this);
        DisabledCardSelection();
    }

    public void EnabledCardSelection()
    {
        hunterHand.EnabledCardSelection();
    }

    public void DisabledCardSelection()
    {
        hunterHand.DisabledCardSelection();
    }

    public void RecoverCards()
    {
        foreach(var discartedCard in DiscartStackSystem.RecoverCards())
        {
            hunterHand.AddCard(discartedCard);
        }
    }

    public void AddEchoes(int amount)
    {
        stockSystem.Add(amount);
    }

    public void RemoveEchoes(int amount)
    {
        stockSystem.Remove(amount);
    }

    public void AddCardToHand(HunterCardSO hunterCardSO)
    {
        hunterHand.AddCard(hunterCardSO);
    }

    public void ChooseCard(HunterCardSO card)
    {
        CurrentCard = Optional<HunterCardSO>.Some(card);
        if(hunterCardReferencePoint.childCount > 0)
        {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);
        }

        var cardGO = Instantiate(card.cardPrefab, hunterCardReferencePoint);
        cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        cardGO.transform.localPosition = new Vector3(0, 0, -1);
        cardGO.GetComponent<HunterCard>().Setup(card);
    }

    public void ChooseHuntersDreamCard()
    {
        ChooseCard(hunterHand.huntersDreamCard);
        hunterHand.CanGoToHuntersDream = false;
    }

    public void UpdateCanGoToHuntersDream(){
        hunterHand.CanGoToHuntersDream = true;
    }

    public void ChooseCard(int cardIndex)
    {
        hunterHand.SetSelectedCard(cardIndex);
        ChooseCard(hunterHand.SelectedCard.HunterCard);
    }

    public void DiscartHunterDreamCard()
    {
        CurrentCard.Some(currentCard => {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);
            CurrentCard = Optional<HunterCardSO>.None();
        });
    }

    public void DiscartCard()
    {
        CurrentCard.Some((currentCard) => {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);

            hunterHand.DiscartSelectedCard();
            DiscartStackSystem.DiscartCard(currentCard);

            CurrentCard = Optional<HunterCardSO>.None();
        });
    }
}
