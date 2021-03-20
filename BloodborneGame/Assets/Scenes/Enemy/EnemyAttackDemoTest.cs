using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackDemoTest : MonoBehaviour {

    public EnemyListSO availableEnemies;

    public Button randomCardButton;
    public Button enemyDamageButton;
    public Button enemyAttackButton;

    public Transform cardSpawn;

    void Awake() {

        availableEnemies = Resources.Load<EnemyListSO>("AllEnemies");

        randomCardButton.onClick.AddListener(() => {
            if(cardSpawn.childCount > 0) Destroy(cardSpawn.GetChild(0).gameObject);

            var enemy = availableEnemies.enemies[Random.Range(0, availableEnemies.enemies.Count)];
            var enemyCard = Instantiate(
                enemy.enemyCardPrefab, 
                new Vector3(0, 0, 0), 
                Quaternion.identity, 
                cardSpawn
            );
            enemyCard.GetComponent<Enemy>().Setup(enemy);

        });

        enemyAttackButton.onClick.AddListener(() => {
            var enemyCard = cardSpawn.GetChild(0);
            if(enemyCard == null) Debug.Log("Instancie uma carta de inimigo antes");

            enemyCard.GetComponent<Enemy>().Attack();
        });

    }

    // Update is called once per frame
    void Update() {

    }
}
