using Assets.UnityFoundation.HealthSystem;
using System;
using UnityEngine;

public class Hunter : MonoBehaviour {
    private Transform hunterCardReferencePoint;
    public Optional<HunterCardSO> CurrentCard { get; private set; }

    public HealthSystem HealthSystem { get; private set; }
    private StockSystem stockSystem;
    private DiscartStackSystem discartStackSystem;

    private Transform hunterHandTransform;
    private HunterHand hunterHand;

    private void Awake() {
        hunterCardReferencePoint = transform.Find("hunterCardReference");
    }

    void Start() {
        HealthSystem = GetComponent<HealthSystem>();
        stockSystem = transform.Find("echoesStock").GetComponent<StockSystem>();
        discartStackSystem = transform.Find("discartStack").GetComponent<DiscartStackSystem>();

        CurrentCard = Optional<HunterCardSO>.None();

        HealthSystem.OnDied += (sender, args) => {
            stockSystem.RemoveAll();
            DiscartCard();
        };

        hunterHandTransform = transform.Find("hunterHand");
        hunterHand = hunterHandTransform.GetComponent<HunterHand>();
        DisabledCardSelection();
    }

    public void EnabledCardSelection() {
        hunterHandTransform.gameObject.SetActive(true);
    }

    public void DisabledCardSelection() {
        hunterHandTransform.gameObject.SetActive(false);
    }


    public void AddEchoes(int amount) {
        stockSystem.Add(amount);
    }

    public void ChooseCard(HunterCardSO card) {
        CurrentCard = Optional<HunterCardSO>.Some(card);
        if(hunterCardReferencePoint.childCount > 0) {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);
        }

        var cardGO = Instantiate(card.cardPrefab, hunterCardReferencePoint);
        cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        cardGO.transform.localPosition = new Vector3(0, 0, -1);
        cardGO.GetComponent<HunterCard>().Setup(card);
    }

    public void ChooseCard(int cardIndex) {
        hunterHand.SetSelectedCard(cardIndex);
        ChooseCard(hunterHand.SelectedCard.HunterCard);
    }

    public void DiscartCard() {
        CurrentCard.Some((currentCard) => {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);

            hunterHand.DiscartCard(currentCard);
            discartStackSystem.DiscartCard(currentCard);

            CurrentCard = Optional<HunterCardSO>.None();
        });
    }
}
