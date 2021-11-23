using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseCardsDemoManager : MonoBehaviour
{

    [SerializeField] private Button enableCardSelectionButton;
    [SerializeField] private Button confirmCardSelectionButton;

    private List<Hunter> hunters;

    void Start()
    {
        var huntersGO = GameObject.FindGameObjectsWithTag(Tags.hunter);
        hunters = new List<Hunter>();
        foreach(var hunter in huntersGO)
        {
            hunters.Add(hunter.GetComponent<Hunter>());
        }

        enableCardSelectionButton.onClick.AddListener(() => {
            new HuntersChooseCardsTurn(hunters).Execute();
        });
    }
}
