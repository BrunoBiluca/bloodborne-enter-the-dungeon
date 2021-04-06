using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterHand : MonoBehaviour {

    void Awake() {
        transform
            .Find("showHunterCardsButton")
            .GetComponent<Button>()
            .onClick
            .AddListener(() => {
                HunterCardsUI.Instance.Show();
            });
    }
}
