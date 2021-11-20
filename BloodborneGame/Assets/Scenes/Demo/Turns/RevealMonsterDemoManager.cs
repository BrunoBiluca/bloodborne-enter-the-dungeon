using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealMonsterDemoManager : MonoBehaviour
{

    [SerializeField] private Button setupMonsterDeckButton;
    [SerializeField] private Button revealMonsterButton;

    void Start()
    {
        var huntersGO = GameObject.FindGameObjectsWithTag(Tags.hunter);
        var hunters = new List<Hunter>();
        foreach(var hunter in huntersGO)
        {
            hunters.Add(hunter.GetComponent<Hunter>());
        }

        setupMonsterDeckButton.onClick.AddListener(() => {
            EnemyDeckManager.Instance.Setup();
        });

        revealMonsterButton.onClick.AddListener(() => {
            new RevealMonsterTurn(hunters).Execute();
        });
    }
}
