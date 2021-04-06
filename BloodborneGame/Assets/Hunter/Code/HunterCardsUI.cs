using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCardsUI : Singleton<HunterCardsUI> {

    protected override void OnAwake() {
        base.OnAwake();

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
