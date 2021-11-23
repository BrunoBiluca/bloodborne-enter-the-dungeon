using Assets.UnityFoundation.Systems.HealthSystem;
using System;
using TMPro;
using UnityEngine;

public class CommonEnemy : EnemyBase
{
    private EnemySO enemySO;

    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private TMP_Text echoesCount;
    private SpriteRenderer lanternIcon;

    public EnemyEffect effect;

    public override void Setup(EnemySO enemySO)
    {
        this.enemySO = enemySO;

        var frontFace = transform.Find("front");

        frontFace.Find("enemyPicture")
            .GetComponent<MeshRenderer>().material = enemySO.enemyPicture;

        nameText = frontFace.Find("name").GetComponent<TMP_Text>();
        nameText.text = enemySO.enemyName;

        descriptionText = frontFace.Find("description").GetComponent<TMP_Text>();
        if(string.IsNullOrEmpty(enemySO.description))
        {
            descriptionText.gameObject.SetActive(false);
        }
        else
        {
            descriptionText.text = enemySO.description;
        }

        echoesCount = frontFace.Find("echoesCount").GetComponent<TMP_Text>();
        echoesCount.text = enemySO.echoesCounter.ToString();

        lanternIcon = frontFace.Find("lanternIcon").GetComponent<SpriteRenderer>();
        lanternIcon.sprite = enemySO.lantern.lanternSprite;

        HealthSystem.Setup(enemySO.echoesCounter);

        if(enemySO.effect != null)
        {
            effect = (EnemyEffect)gameObject.AddComponent(enemySO.effect.GetType());
            effect.Parameters = enemySO.parameters;
        }
    }

    public override void Attack(Action<int> attackFinished)
    {
        EnemyAttackHandler.Instance
            .Handle(enemySO.lantern, (totalDamage) => attackFinished(totalDamage));
    }

    public override void Damage(int amount, Action<int> damageFinished)
    {
        var damageAmount = HealthSystem.CurrentHealth - amount < 0
            ? (int)HealthSystem.CurrentHealth
            : amount;

        HealthSystem.Damage(damageAmount);
        damageFinished(damageAmount);
    }

    public override EnemyEffect GetEffect()
    {
        return effect;
    }

    public override EnemySO GetEnemySO()
    {
        return enemySO;
    }
}
