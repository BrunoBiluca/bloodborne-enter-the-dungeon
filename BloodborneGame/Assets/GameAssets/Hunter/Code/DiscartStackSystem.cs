using Assets.UnityFoundation.Code;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscartStackSystem : MonoBehaviour
{
    public List<HunterCardSO> DiscartedCards;

    private Button discartStackButton;

    private TextMeshProUGUI discartCounterText;

    private void Awake()
    {
        DiscartedCards = new List<HunterCardSO>();

        discartStackButton = transform.FindComponent<Button>(
            "hunter_canvas",
            "discart_pile_button"
        );

        discartCounterText = transform.FindComponent<TextMeshProUGUI>(
            "hunter_canvas",
            "discart_pile_button",
            "card_number",
            "text"
        );
    }

    public void DiscartCard(HunterCardSO card)
    {
        DiscartedCards.Add(card);

        discartCounterText.text = DiscartedCards.Count.ToString();
    }

    public IEnumerable<HunterCardSO> RecoverCards(){
        foreach (var card in DiscartedCards)
        {
            yield return card;
        }

        DiscartedCards.Clear();
        discartCounterText.text = DiscartedCards.Count.ToString();
    }
}
