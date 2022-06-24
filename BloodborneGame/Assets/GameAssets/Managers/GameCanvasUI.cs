using UnityFoundation.Code;
using TMPro;

public class GameCanvasUI : Singleton<GameCanvasUI>
{
    private TextMeshProUGUI currentRound;
    private TextMeshProUGUI currentTurnText;

    protected override void OnAwake()
    {
        currentRound = transform.Find("game_panel")
            .Find("current_round_text")
            .GetComponent<TextMeshProUGUI>();

        currentTurnText = transform.Find("game_panel")
            .Find("current_turn_text")
            .GetComponent<TextMeshProUGUI>();
    }

    public void ChangeTurn(string turnName)
    {
        currentTurnText.text = "Turn: " + turnName;
    }

    public void UpdateRound(int newRound)
    {
        currentRound.text = "Round: " + newRound.ToString();
    }
}
