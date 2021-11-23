using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.TimeUtils;
using System.Collections;

class MonsterEscapeTurn : ITurn
{
    private readonly GameManager gameManager;

    private bool turnFinished;

    public MonsterEscapeTurn(GameManager gameManager)
    {
        this.gameManager = gameManager;
        turnFinished = false;
    }

    public void Execute()
    {
        if(!CanEnemyEscape())
        {
            turnFinished = true;
            return;
        }

        var enemy = gameManager.CurrentEnemy.Get();

        if(enemy.GetEffect() is IWhenEscapeEffect effect)
        {
            if(effect is DamageWhenEscapeEffect ef)
            {
                ef.Setup(gameManager.GetAliveHunters()).Handle();
            }
        }

        gameManager.StartCoroutine(ReturnMonsterCoroutine(enemy));
    }

    private bool CanEnemyEscape()
    {
        if(!gameManager.CurrentEnemy.IsPresentAndGet(out EnemyBase enemy))
            return false;

        if(enemy.GetEnemySO().isBoss)
            return false;

        return true;
    }

    public IEnumerator ReturnMonsterCoroutine(EnemyBase enemy)
    {
        EnemySpawner.Instance.RemoveEnemy();
        EnemyDeckManager.Instance.ReturnMonster(enemy.GetEnemySO());

        yield return WaittingCoroutine.UntilPropertyTrue(
            EnemyDeckManager.Instance,
            nameof(EnemyDeckManager.Instance.IsDeckSetupFinished)
        );

        gameManager.CurrentEnemy = Optional<EnemyBase>.None();
        turnFinished = true;
    }

    public bool IsTurnFinished()
    {
        return turnFinished;
    }
}
