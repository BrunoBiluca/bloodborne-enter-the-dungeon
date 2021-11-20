using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;

public class HunterCardsUI : Singleton<HunterCardsUI>
{

    private Camera mainCamera;
    private Transform hunterCardsHolder;

    private Hunter hunter;
    private HunterHand hunterHand;

    protected override void OnAwake()
    {
        base.OnAwake();

        mainCamera = Camera.main;
        hunterCardsHolder = transform.Find("hunterCardsHolder");
        Hide();
    }

    public void Show(HunterHand hunterHand, Hunter hunter)
    {
        gameObject.SetActive(true);

        var cardOffset = 100;
        var mostLeftCardOffset = -50 * (hunterHand.AvailableCards.Count - 1);

        foreach(var card in hunterHand.AvailableCards)
        {
            var cardGO = Instantiate(card.HunterCard.cardPrefab, hunterCardsHolder);
            cardGO.name = card.Index.ToString();
            Destroy(cardGO.GetComponent<Rigidbody>());

            cardGO.GetComponent<HunterCard>().Setup(card.HunterCard);
            cardGO.transform.localPosition = new Vector3(mostLeftCardOffset, 0, 0);
            mostLeftCardOffset += cardOffset;

            cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            cardGO.transform.localScale = new Vector3(10, 1, 10);
        }

        this.hunterHand = hunterHand;
        this.hunter = hunter;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        var cards = new List<GameObject>();

        foreach(Transform child in hunterCardsHolder)
        {
            if(child.CompareTag(Tags.hunter_card))
                cards.Add(child.gameObject);
        }

        foreach(var card in cards)
        {
            Destroy(card);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.transform.gameObject.TryGetComponent(out HunterCard hunterCard))
                {
                    Debug.Log(hit.transform.name);
                    hunter.ChooseCard(int.Parse(hit.transform.name));
                }
                Hide();
            }
        }
    }
}
