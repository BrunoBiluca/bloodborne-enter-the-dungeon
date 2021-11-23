using Assets.UnityFoundation.Code;
using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDeckManager : Singleton<EnemyDeckManager>
{

    private EnemySO finalBoss;
    private List<EnemySO> monsters;

    public bool IsDeckSetupFinished { get; private set; }

    public void Setup(int enemyCount = 7)
    {
        var availableEnemies = Resources.Load<EnemyListSO>("all_enemies_so");

        monsters = new List<EnemySO>();

        var banishedMonsters = new List<int>();
        while(monsters.Count < enemyCount)
        {
            var randomMonster = Random.Range(0, availableEnemies.enemies.Count);

            if(banishedMonsters.Contains(randomMonster))
                continue;

            monsters.Add(availableEnemies.enemies[randomMonster]);
            banishedMonsters.Add(randomMonster);
        }
        StartCoroutine(InstantiateDeck());
    }

    private IEnumerator InstantiateDeck()
    {
        IsDeckSetupFinished = false;

        TransformUtils.RemoveChildObjects(transform);

        monsters.Shuffle();

        foreach(var monster in monsters)
        {
            var go = Instantiate(GameAssets.Instance.enemyCardOnlyCover, transform);
            go.transform.localPosition = new Vector3(0, 0.5f, 0);
            yield return WaittingCoroutine.RealSeconds(0.3f);
        }

        IsDeckSetupFinished = true;
    }

    public EnemyBase RevealMonster()
    {
        if(monsters.Count == 0)
        {
            // TODO: nesse caso deverá mostrar o chefão final
            return null;
        }

        var revealedMonster = monsters.Last();
        monsters.Remove(revealedMonster);

        Destroy(transform.GetChild(transform.childCount - 1).gameObject);

        return EnemySpawner.Instance.InstantiateEnemy(revealedMonster);
    }

    public void ReturnMonster(EnemySO enemy)
    {
        monsters.Add(enemy);
        StartCoroutine(InstantiateDeck());
    }
}
