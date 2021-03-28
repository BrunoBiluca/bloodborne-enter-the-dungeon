using Assets.UnityFoundation.HealthSystem;
using UnityEngine;

public class Hunter : MonoBehaviour {
    [SerializeField] private Transform hunterCardReferencePoint;
    public Optional<HunterCardSO> CurrentCard { get; private set; }

    private HealthSystem healthSystem;
    private StockSystem stockSystem;

    void Start() {
        healthSystem = GetComponent<HealthSystem>();
        stockSystem = transform.Find("echoesStock").GetComponent<StockSystem>();

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
        cardGO.GetComponent<HunterCard>().Setup(card);
    }

    public void DiscartCard() {
        CurrentCard.Some((currentCard) => {
            Destroy(hunterCardReferencePoint.GetChild(0).gameObject);
            CurrentCard = Optional<HunterCardSO>.None();
        });
    }
}
