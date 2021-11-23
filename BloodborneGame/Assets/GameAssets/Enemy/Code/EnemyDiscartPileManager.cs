using Assets.UnityFoundation.Code.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiscartPileManager : Singleton<EnemyDiscartPileManager>
{
    private List<EnemyBase> enemies;

    protected override void OnAwake()
    {
        enemies = new List<EnemyBase>();
    }

    public void Discart(EnemyBase enemy)
    {
        enemies.Add(enemy);

        enemy.transform.parent = transform.Find("plane");
        enemy.transform.localPosition = new Vector3(0f, 1f, 0f);
    }
}
