using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAttackDemoManager : MonoBehaviour
{

    [SerializeField] private Button randomEnemyButton;
    [SerializeField] private Button addEchoButton;
    [SerializeField] private Button executeTurnButton;

    void Start()
    {
        randomEnemyButton.onClick.AddListener(() => {
            EnemySpawner.Instance.SpawnRandomEnemy();
        });

        addEchoButton.onClick.AddListener(() => {
            var hunters = GameObject.FindGameObjectsWithTag(Tags.hunter);
            foreach(var hunter in hunters)
            {
                hunter.GetComponent<Hunter>().AddEchoes(1);
            }
        });

        executeTurnButton.onClick.AddListener(() => {
        });
    }

}
