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
            DiceRollManager.Instance.Roll(greenDice, (side) => {
                Debug.Log(side);
            });
        });

        yellowRollButton.onClick.AddListener(() => {
            DiceRollManager.Instance.Roll(yellowDice, (side) => {
                Debug.Log(side);
            });
        });

        redRollButton.onClick.AddListener(() => {
            DiceRollManager.Instance.Roll(redDice, (side) => {
                Debug.Log(side);
            });
        });

    }
}
