using UnityFoundation.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntersAttackDemoManager : MonoBehaviour
{

    [SerializeField] private Button randomEnemyButton;
    [SerializeField] private Button chooseHunterCardButton;
    [SerializeField] private Button executeTurnButton;

    [SerializeField] private Transform enemyCardSpawn;

    [SerializeField] private Hunter hunter;

    void Start()
    {

        var cards = Resources.Load<HunterCardListSO>("HunterCards").cards;

        randomEnemyButton.onClick.AddListener(() => {
            EnemySpawner.Instance.SpawnRandomEnemy();
        });

        chooseHunterCardButton.onClick.AddListener(() => {
            hunter.ChooseCard(cards[Random.Range(0, cards.Length)]);
        });

        executeTurnButton.onClick.AddListener(() => {
            var enemy = EnemySpawner.Instance.GetEnemy();
            if(enemy == null) return;

            new HuntersAttackTurn((GameManager)GameManager.Instance).Execute();
        });
    }
}
