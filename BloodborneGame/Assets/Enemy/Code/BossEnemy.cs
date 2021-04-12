using Assets.UnityFoundation.HealthSystem;
using System;
using TMPro;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IEnemy {

    private EnemySO enemySO;

    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private TMP_Text echoesCount;
    private SpriteRenderer lanternIcon;
    private HealthSystem healthSystem;
    private EnemyEffect effect;

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

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Setup(enemySO.echoesCounter);

        if(enemySO.effect != null) {
            effect = (EnemyEffect)gameObject.AddComponent(enemySO.effect.GetType());
            effect.Parameters = enemySO.parameters;
        }
    }

    public void Attack(Action attackFinished) {
        EnemyAttackHandler.Instance.Handle(enemySO.lantern, (totalDamage) => {
            attackFinished();
        });
    }

    public void Damage(int amount, Action<int> damageFinished) {
        healthSystem.Damage(amount);
        damageFinished(amount);
    }

    public EnemyEffect GetEffect() {
        return effect;
    }

    public EnemySO GetEnemySO() {
        return enemySO;
    }
}
