using Assets.UnityFoundation.Code.Common;
using UnityEngine;

public class HunterCardDeck : Singleton<HunterCardDeck>
{
    private HunterCardSO[] cards;

    protected override void OnAwake()
    {
        cards = Resources.Load<HunterCardListSO>("HunterCards").cards;
    }

    public HunterCardSO GetRandomCard()
    {
        return cards[Random.Range(0, cards.Length)];
    }
}
