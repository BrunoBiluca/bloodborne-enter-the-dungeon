using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterEscapeDemoManager : MonoBehaviour
{

    [SerializeField] private Button setupDeckButton;
    [SerializeField] private Button invokeMonsterButton;
    [SerializeField] private Button invokeBossButton;
    [SerializeField] private Button invokeEffectMonsterButton;
    [SerializeField] private Button executeTurnButton;

    private List<EnemySO> commonMonsters = new List<EnemySO>();
    private List<EnemySO> bossMonsters = new List<EnemySO>();
    private List<EnemySO> effectMonsters = new List<EnemySO>();

    private List<Hunter> hunters;

    void Start()
    {
        var allEnemies = Resources.Load<EnemyListSO>("all_enemies_so");

        commonMonsters.AddRange(allEnemies.enemies.FindAll(e => !e.isBoss));
        bossMonsters.AddRange(allEnemies.enemies.FindAll(e => e.isBoss));
        effectMonsters.AddRange(
            allEnemies.enemies.FindAll(e => {
                if(e.effect is IWhenEscapeEffect)
                    return true;

                return false;
            })
        );

        var enemy = GameObject.FindGameObjectWithTag(Tags.enemy)
            .GetComponent<EnemyBase>();

        hunters = GameObject
            .FindGameObjectsWithTag(Tags.hunter)
            .Select(hunter => hunter.GetComponent<Hunter>())
            .ToList();

        setupDeckButton.onClick.AddListener(() => {
            EnemyDeckManager.Instance.Setup(0);
        });

        invokeMonsterButton.onClick.AddListener(() => {
            var monster = commonMonsters[Random.Range(0, commonMonsters.Count)];
            EnemySpawner.Instance.InstantiateEnemy(monster);
        });

        invokeBossButton.onClick.AddListener(() => {
            var monster = bossMonsters[Random.Range(0, bossMonsters.Count)];
            EnemySpawner.Instance.InstantiateEnemy(monster);
        });

        invokeEffectMonsterButton.onClick.AddListener(() => {
            var monster = effectMonsters[Random.Range(0, effectMonsters.Count)];
            EnemySpawner.Instance.InstantiateEnemy(monster);
        });

        executeTurnButton.onClick.AddListener(() => {
        });
    }
}
