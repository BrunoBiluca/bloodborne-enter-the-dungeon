using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.Code.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTurnsUI : Singleton<GameTurnsUI>
{
    private List<Transform> turnsReferences;

    private TextMeshProUGUI roundCounterText;

    private TextMeshProUGUI timeCounterText;

    protected override void OnAwake()
    {
        turnsReferences = new List<Transform>();

        foreach(Transform turnPrefab in transform.FindTransform("turns_panel", "turns_holder"))
        {
            turnsReferences.Add(turnPrefab);
        }

        foreach(var turnRef in turnsReferences)
        {
            turnRef.Find("active").gameObject.SetActive(false);
        }

        roundCounterText = transform.FindComponent<TextMeshProUGUI>(
            "stats_panel", "round_counter", "value_text"
        );

        timeCounterText = transform.FindComponent<TextMeshProUGUI>(
            "stats_panel", "timer", "value_text"
        );

    }

    public void ChangeTurn(int index)
    {
        foreach(var turnRef in turnsReferences)
        {
            turnRef.Find("active").gameObject.SetActive(false);
        }

        turnsReferences[index].Find("active").gameObject.SetActive(true);
    }

    public void UpdateTimeCounter(float timePassed)
    {
        timeCounterText.text = TimeSpan.FromSeconds(timePassed).ToString(@"mm\:ss");
    }

    public void UpdateRoundCounter(int round)
    {
        roundCounterText.text = round.ToString();
    }
}
