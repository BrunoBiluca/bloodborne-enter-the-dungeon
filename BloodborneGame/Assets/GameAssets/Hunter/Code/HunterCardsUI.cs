using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;

public class HunterCardsUI : Singleton<HunterCardsUI>
{
    private Transform hunterCardsHolder;

    private Hunter hunter;
    private HunterHand hunterHand;

    protected override void OnAwake()
    {
        base.OnAwake();

        var canvas = GetComponent<Canvas>();
        if(canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;

        hunterCardsHolder = transform.Find("hunter_cards_holder");
        Hide();
    }

    public void Show(HunterHand hunterHand, Hunter hunter)
    {
        gameObject.SetActive(true);

        var cardOffset = 200;
        var mostLeftCardOffset = -100 * (hunterHand.AvailableCards.Count - 1);

        TransformUtils.RemoveChildObjects(hunterCardsHolder);

        var index = 0;
        foreach(var card in hunterHand.AvailableCards)
        {
            var cardGO = Instantiate(card.HunterCard.cardPrefab, hunterCardsHolder);
            cardGO.name = (index++).ToString();
            Destroy(cardGO.GetComponent<Rigidbody>());

            cardGO.GetComponent<HunterCard>().Setup(card.HunterCard);
            cardGO.transform.localPosition = new Vector3(mostLeftCardOffset, 0, -0.5f);
            mostLeftCardOffset += cardOffset;

            cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            cardGO.transform.localScale = new Vector3(20, 1, 20);
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
            if(PhysicsUtils.RaycastType(
                    Input.mousePosition,
                    out HunterCard hunterCard,
                    LayerMask.NameToLayer("UI")
                ))
            {
                hunter.ChooseCard(int.Parse(hunterCard.name));
            }
            Hide();
        }
    }
}
