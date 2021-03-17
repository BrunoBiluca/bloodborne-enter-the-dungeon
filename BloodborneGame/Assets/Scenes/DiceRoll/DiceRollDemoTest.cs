using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollDemoTest : MonoBehaviour {

    public Button rollButton;

    void Start() {
        rollButton.onClick.AddListener(() => {
            Debug.Log(DiceRollManager.Instance.Roll());
        });
    }
}
