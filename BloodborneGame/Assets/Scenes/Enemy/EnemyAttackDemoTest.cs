using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackDemoTest : MonoBehaviour {

    public EnemyListSO availableEnemies;

    public Button randomCardButton;
    public Button enemyDamageButton;
    public Button enemyAttackButton;
    public Button callHunterButton;

    public Transform cardSpawn;

    public Transform hunterHolder;
    public GameObject hunterPrefab;

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
            enemyCard.GetComponent<IEnemy>().Setup(enemy);

        });

        enemyAttackButton.onClick.AddListener(() => {
            var enemyCard = cardSpawn.GetChild(0);
            if(enemyCard == null) {
                Debug.Log("Instancie uma carta de inimigo antes");
                return;
            }

            enemyCard.GetComponent<IEnemy>().Attack(() => {
                Debug.Log("Attack done");
            });
        });

        enemyDamageButton.onClick.AddListener(() => {
            var enemyCard = cardSpawn.GetChild(0);
            if(enemyCard == null) {
                Debug.Log("Instancie uma carta de inimigo antes");
                return;
            }

            enemyCard.GetComponent<IEnemy>().Damage(1, () => {
                Debug.Log("Damage finished");
            });
        });

        callHunterButton.onClick.AddListener(() => {
            if(hunterHolder.childCount > 0) {
                Destroy(hunterHolder.GetChild(0).gameObject);
            }

            Instantiate(hunterPrefab, hunterHolder);
        });

    }
}
