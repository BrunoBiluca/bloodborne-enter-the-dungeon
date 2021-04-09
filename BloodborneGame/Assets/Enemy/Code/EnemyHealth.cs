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

        var removeEchoesList = new List<int>();

        var removeEchoes = transform.childCount - (int)currentHealth;
        while(removeEchoesList.Count < removeEchoes) {
            var randomEcho = Random.Range(0, transform.childCount);
            if(!removeEchoesList.Contains(randomEcho)) {
                removeEchoesList.Add(randomEcho);
            }
        }

        foreach(var id in removeEchoesList) {
            Destroy(transform.GetChild(id).gameObject);
        }
        

    }

}
