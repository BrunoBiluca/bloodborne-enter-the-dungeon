using UnityEngine;
using UnityEngine.UI;

public class ImmediateEffectDemoManager : MonoBehaviour
{

    [SerializeField] private Button randomEnemyButton;
    [SerializeField] private Button playerChooseCardButton;
    [SerializeField] private Button executeTurnButton;

    [SerializeField] private Hunter hunter;

    void Start()
    {
        randomEnemyButton.onClick.AddListener(() => {
            EnemySpawner.Instance.SpawnRandomEnemy();
        });
        playerChooseCardButton.onClick.AddListener(() => {
            hunter.ChooseCard(0);
        });
        executeTurnButton.onClick.AddListener(() => {
            var enemy = EnemySpawner.Instance.GetEnemy();
            if(enemy == null) return;

            new ImmediateEffectTurn((GameManager)GameManager.Instance).Execute();
        });
    }
}
