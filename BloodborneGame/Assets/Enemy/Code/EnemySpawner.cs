using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private Transform cardSpawnReference;

    private EnemyListSO availableEnemies;

    private void Awake() {
        Instance = this;
        availableEnemies = Resources.Load<EnemyListSO>("AllEnemies");
    }

    public IEnemy SpawnRandomEnemy() {
        if(cardSpawnReference.childCount > 0) 
            Destroy(cardSpawnReference.GetChild(0).gameObject);

        var enemySO = availableEnemies.enemies[Random.Range(0, availableEnemies.enemies.Count)];
        var enemyCard = Instantiate(
            enemySO.enemyCardPrefab,
            cardSpawnReference
        );
        IEnemy enemy = enemyCard.GetComponent<IEnemy>();
        enemy.Setup(enemySO);

        return enemy;
    }

    public IEnemy GetEnemy() {
        if(cardSpawnReference.childCount == 0) return null;

        return cardSpawnReference.GetChild(0).gameObject.GetComponent<IEnemy>();
    }
}
