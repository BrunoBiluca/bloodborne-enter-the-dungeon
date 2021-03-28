using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackDemoTest : MonoBehaviour {

    public Button randomCardButton;
    public Button enemyDamageButton;
    public Button enemyAttackButton;
    public Button callHunterButton;

    public Transform cardSpawn;

    public Transform hunterHolder;
    public GameObject hunterPrefab;

    void Awake() {
        randomCardButton.onClick.AddListener(() => {
            EnemySpawner.Instance.SpawnRandomEnemy();
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

            enemyCard.GetComponent<IEnemy>().Damage(1, (damage) => {
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
