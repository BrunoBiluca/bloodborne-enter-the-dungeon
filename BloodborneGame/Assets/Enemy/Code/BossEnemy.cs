using Assets.UnityFoundation.TimeUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IEnemy {

    // TODO: buscar esse prefab depois do GameAssets
    [SerializeField] private GameObject echoesPrefab;
    [SerializeField] private Transform spawnEchoesReference;

    private EnemySO enemySO;

    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private TMP_Text echoesCount;
    private SpriteRenderer lanternIcon;

    public void Setup(EnemySO enemySO) {
        this.enemySO = enemySO;

        var frontFace = transform.Find("front");

        frontFace.Find("bossPicture")
            .GetComponent<MeshRenderer>().material = enemySO.enemyPicture;

        nameText = frontFace.Find("name").GetComponent<TMP_Text>();
        nameText.text = enemySO.enemyName;

        descriptionText = frontFace.Find("description").GetComponent<TMP_Text>();
        if(string.IsNullOrEmpty(enemySO.description)) {
            descriptionText.gameObject.SetActive(false);
        } else {
            descriptionText.text = enemySO.description;
        }

        echoesCount = frontFace.Find("echoesCount").GetComponent<TMP_Text>();
        echoesCount.text = enemySO.echoesCounter.ToString();

        lanternIcon = frontFace.Find("lanternIcon").GetComponent<SpriteRenderer>();
        lanternIcon.sprite = enemySO.lantern.lanternSprite;

        spawnEchoesReference = transform.Find("spawnEchoesReference");
        StartCoroutine(SpawnEchoes());
    }

    private IEnumerator SpawnEchoes() {
        for(var i = 0; i < enemySO.echoesCounter; i++) {
            Instantiate(
                echoesPrefab,
                spawnEchoesReference.position,
                Quaternion.identity,
                transform
            );

            yield return WaittingCoroutine.RealSeconds(.2f);
        }
    }

    public void Attack(Action attackFinished) {
        EnemyAttackHandler.Instance.Handle(enemySO.lantern, (totalDamage) => {
            attackFinished();
        });
    }

}
