using Assets.UnityFoundation.Code.Common;
using UnityEngine;

public class HunterCardDeckManager : Singleton<HunterCardDeckManager>
{
    private HunterCardSO[] cards;

    private int baseCardCost = 4;

    private int initialCardNumber = 5;

    protected override void OnAwake()
    {
        cards = Resources.Load<HunterCardListSO>("hunter_card_list_so").cards;
    }

    public HunterCardSO GetRandomCard()
    {
        return cards[Random.Range(0, cards.Length)];
    }

    public int NextCardCost(Hunter hunter) {
        return baseCardCost + hunter.HunterHandCardsCount - initialCardNumber;
    }

    public HunterCardSO BuyCard(Hunter hunter){
        var cardCost = NextCardCost(hunter);

        if(hunter.EchoesCount >= cardCost){
            var card = GetRandomCard();
            hunter.RemoveEchoes(cardCost);
            hunter.AddCardToHand(card);
            return card;
        }

        return null;
    }
}
