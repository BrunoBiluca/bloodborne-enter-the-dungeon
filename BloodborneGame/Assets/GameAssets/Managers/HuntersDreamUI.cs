using Assets.UnityFoundation.Code;
using UnityFoundation.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HuntersDreamUI : Singleton<HuntersDreamUI>
{
    [SerializeField] private Transform newCardCanvas;

    private List<Button> buyCardButtons;

    public event Action OnFinished;

    protected override void OnAwake()
    {
        buyCardButtons = transform
            .FindComponentsInChildren<Button>("dialogue_panel.buy_card_buttons")
            .ToList();

        transform.FindComponent<Button>("dialogue_panel.quit_button")
            .onClick
            .AddListener(() => {
                OnFinished?.Invoke();
                Hide();
            });

        Hide();
    }

    public void Show(List<Hunter> huntersOnDream)
    {
        foreach(var button in buyCardButtons)
        {
            var hunterConfig = button.GetComponent<HunterConfigHolder>().HunterConfig;
            var hunter = huntersOnDream.Find(h => h.hunterConfig == hunterConfig);

            if(hunter != null)
            {
                UpdateBuyCardButton(button, hunter);

                button.interactable = true;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => {
                    var newCard = HunterCardDeckManager.Instance.BuyCard(hunter);

                    if(newCard != null)
                    {
                        ShowNewCard(newCard);
                    }

                    UpdateBuyCardButton(button, hunter);
                });
            }
            else
            {
                button.interactable = false;
            }
        }

        gameObject.SetActive(true);
    }

    private void ShowNewCard(HunterCardSO card)
    {
        var newCardHolder = newCardCanvas.FindTransform("new_card_panel", "hunter_card_holder");

        TransformUtils.RemoveChildObjects(newCardHolder);

        var cardGO = Instantiate(card.cardPrefab, newCardHolder);
        Destroy(cardGO.GetComponent<Rigidbody>());

        cardGO.GetComponent<HunterCard>()
            .Setup(card);
            
        cardGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        cardGO.transform.localScale = new Vector3(17, 1, 17);

        newCardCanvas.gameObject.SetActive(true);

        StartCoroutine(HideNewCard());
    }

    private IEnumerator HideNewCard()
    {
        yield return new WaitForSeconds(2f);
        newCardCanvas.gameObject.SetActive(false);
    }

    private void UpdateBuyCardButton(Button button, Hunter hunter)
    {
        var cardCost = HunterCardDeckManager.Instance.NextCardCost(hunter);
        button.transform
            .FindComponent<TextMeshProUGUI>("text")
            .text = @$"Buy card (<color=""red"">{cardCost}</color>)";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        newCardCanvas.gameObject.SetActive(false);
    }
}
