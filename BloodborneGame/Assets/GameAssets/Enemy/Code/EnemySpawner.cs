using UnityFoundation.Code;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{

    [SerializeField] private Transform cardSpawnReference;

    private EnemyListSO availableEnemies;

    protected override void OnAwake()
    {
        base.OnAwake();

        availableEnemies = Resources.Load<EnemyListSO>("all_enemies_so");

        if(cardSpawnReference == null) cardSpawnReference = transform;
    }

    public EnemyBase SpawnRandomEnemy()
    {
        int randomIdx = Random.Range(0, availableEnemies.enemies.Count);
        var enemySO = availableEnemies.enemies[randomIdx];
        return InstantiateEnemy(enemySO);
    }

    public EnemyBase GetEnemy()
    {
        if(cardSpawnReference.childCount == 0) return null;

        return cardSpawnReference.GetChild(0).gameObject.GetComponent<EnemyBase>();
    }

    public EnemyBase InstantiateEnemy(EnemySO enemySO)
    {
        if(cardSpawnReference.childCount > 0)
            Destroy(cardSpawnReference.GetChild(0).gameObject);

        var enemyCard = Instantiate(
            enemySO.enemyCardPrefab,
            cardSpawnReference
        );
        EnemyBase enemy = enemyCard.GetComponent<EnemyBase>();
        enemy.Setup(enemySO);

        return enemy;
    }

    public void RemoveEnemy()
    {
        if(cardSpawnReference.childCount == 0) return;

        Destroy(cardSpawnReference.GetChild(0).gameObject);
    }

}
