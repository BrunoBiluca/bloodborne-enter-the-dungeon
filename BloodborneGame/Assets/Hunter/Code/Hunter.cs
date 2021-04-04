using Assets.UnityFoundation.HealthSystem;
using UnityEngine;

public class Hunter : MonoBehaviour {
    private Transform hunterCardReferencePoint;
    public Optional<HunterCardSO> CurrentCard { get; private set; }

    public HealthSystem HealthSystem { get; private set; }
    private StockSystem stockSystem;
    private DiscartStackSystem discartStackSystem;

    private void Awake() {
        hunterCardReferencePoint = transform.Find("hunterCardReference");
    }

    void Start() {
        HealthSystem = GetComponent<HealthSystem>();
        stockSystem = transform.Find("echoesStock").GetComponent<StockSystem>();
        discartStackSystem = transform.Find("discartStack").GetComponent<DiscartStackSystem>();

        CurrentCard = Optional<HunterCardSO>.None();
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

    public void DiscartCard() {
        CurrentCard.Some((currentCard) => {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);

            discartStackSystem.DiscartCard(currentCard);

            CurrentCard = Optional<HunterCardSO>.None();
        });
    }
}
