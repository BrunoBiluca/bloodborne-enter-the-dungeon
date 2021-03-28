using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscartStackSystem : MonoBehaviour {

    private readonly List<HunterCardSO> discartedCards = new List<HunterCardSO>();

    public void DiscartCard(HunterCardSO card) {
        var cardGO = Instantiate(card.cardPrefab, transform);
        cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        cardGO.transform.localPosition = new Vector3(0, 0, -1);
        cardGO.GetComponent<HunterCard>().Setup(card);

        discartedCards.Add(card);
    }

}
