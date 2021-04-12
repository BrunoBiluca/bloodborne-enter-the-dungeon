using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner> {

    [SerializeField] private Transform cardSpawnReference;

    private EnemyListSO availableEnemies;

    protected override void OnAwake() {
        base.OnAwake();

        availableEnemies = Resources.Load<EnemyListSO>("AllEnemies");

        if(cardSpawnReference == null) cardSpawnReference = transform;
    }

    public IEnemy SpawnRandomEnemy() {
        var enemySO = availableEnemies.enemies[Random.Range(0, availableEnemies.enemies.Count)];
        return InstantiateEnemy(enemySO);
    }

    public IEnemy GetEnemy() {
        if(cardSpawnReference.childCount == 0) return null;

        return cardSpawnReference.GetChild(0).gameObject.GetComponent<IEnemy>();
    }

    public IEnemy InstantiateEnemy(EnemySO enemySO) {
        if(cardSpawnReference.childCount > 0) 
            Destroy(cardSpawnReference.GetChild(0).gameObject);

        var enemyCard = Instantiate(
            enemySO.enemyCardPrefab,
            cardSpawnReference
        );
        IEnemy enemy = enemyCard.GetComponent<IEnemy>();
        enemy.Setup(enemySO);

        return enemy;
    }

    public void RemoveEnemy() {
        if(cardSpawnReference.childCount == 0) return;
        
        Destroy(cardSpawnReference.GetChild(0).gameObject);
    }

}
