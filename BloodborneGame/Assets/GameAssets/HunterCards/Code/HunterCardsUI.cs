using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.Code.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterCardsUI : Singleton<HunterCardsUI>
{
    private Transform hunterCardsHolder;

    private Hunter hunter;

    private Button huntersDreamButton;

    private Button backgroundButton;

    private List<Vector3> cardPositions = new List<Vector3> {
        new Vector3(-175f, 135f, 0f),
        new Vector3(0f, 135f, 0f),
        new Vector3(175f, 135f, 0f),
        new Vector3(-175f, -50f, 0f),
        new Vector3(0f, -50f, 0f),
        new Vector3(175f, -50f, 0f),
        new Vector3(-100f, -235f, 0f),
        new Vector3(100f, -235f, 0f)
    };

    protected override void OnAwake()
    {
        base.OnAwake();

        var canvas = GetComponent<Canvas>();
        if(canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;

        hunterCardsHolder = transform.FindTransform("hunter_hand_panel.hunter_cards_holder");

        huntersDreamButton = transform
            .FindComponent<Button>("hunter_hand_panel.hunter_dream_button");

        huntersDreamButton.onClick.AddListener(() => {
            hunter.ChooseHuntersDreamCard();
            Hide();
        });

        backgroundButton = transform.FindComponent<Button>("background_button");
        backgroundButton.onClick.AddListener(() => Hide());

        Hide();
    }

    public void Show(HunterHand hunterHand, Hunter hunter)
    {
        gameObject.SetActive(true);

        TransformUtils.RemoveChildObjects(hunterCardsHolder);

        for(int cardIndex = 0; cardIndex < hunterHand.AvailableCards.Count; cardIndex++)
        {
            var card = hunterHand.AvailableCards[cardIndex];

            var cardGO = Instantiate(card.HunterCard.cardPrefab, hunterCardsHolder);
            cardGO.name = (cardIndex).ToString();
            Destroy(cardGO.GetComponent<Rigidbody>());

            cardGO.GetComponent<HunterCard>().Setup(card.HunterCard);

            cardGO.transform.localPosition = cardPositions[cardIndex];
            cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            cardGO.transform.localScale = new Vector3(17, 1, 17);
        }

        huntersDreamButton.interactable = hunterHand.CanGoToHuntersDream;

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
                Hide();
            }
        }
    }
}
