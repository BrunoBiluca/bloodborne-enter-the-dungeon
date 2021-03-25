using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthBar {

    private float baseHealth;

    public void Setup(float baseHealth) {
        this.baseHealth = baseHealth;
        StartCoroutine(SpawnEchoes());
    }

    private IEnumerator SpawnEchoes() {
        for(var i = 0; i < (int)baseHealth; i++) {
            Instantiate(
                GameAssets.Instance.echoesPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );

            yield return WaittingCoroutine.RealSeconds(.2f);
        }
    }

    public void SetCurrentHealth(float currentHealth) {
        if(currentHealth == 0) return;

        var randomEcho = Random.Range(0, transform.childCount);
        Destroy(transform.GetChild(randomEcho).gameObject);
    }

}
