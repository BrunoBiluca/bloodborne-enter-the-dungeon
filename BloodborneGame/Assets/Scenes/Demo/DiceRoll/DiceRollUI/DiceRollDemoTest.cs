using DiceRoll;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollDemoTest : MonoBehaviour
{

    public Button greenRollButton;
    public Button yellowRollButton;
    public Button redRollButton;

    public HunterDiceSO greenDice;
    public HunterDiceSO yellowDice;
    public HunterDiceSO redDice;

    void Start()
    {
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
