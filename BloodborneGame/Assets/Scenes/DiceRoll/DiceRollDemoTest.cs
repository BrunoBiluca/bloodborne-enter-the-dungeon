using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollDemoTest : MonoBehaviour {

    public Button greenRollButton;
    public Button yellowRollButton;
    public Button redRollButton;

    public DiceSO greenDice;
    public DiceSO yellowDice;
    public DiceSO redDice;

    void Start() {
        greenRollButton.onClick.AddListener(() => {
            Debug.Log(DiceRollManager.Instance.Roll(greenDice));
        });

        yellowRollButton.onClick.AddListener(() => {
            Debug.Log(DiceRollManager.Instance.Roll(greenDice));
        });

        redRollButton.onClick.AddListener(() => {
            Debug.Log(DiceRollManager.Instance.Roll(greenDice));
        });

    }
}
