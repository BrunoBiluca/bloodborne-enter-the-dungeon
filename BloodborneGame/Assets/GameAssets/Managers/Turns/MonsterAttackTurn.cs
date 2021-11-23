public class MonsterAttackTurn : ITurn
{
    private readonly GameManager gameManager;
    private bool finishedAttack = false;

    public MonsterAttackTurn(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Execute()
    {
        finishedAttack = false;

        if(!gameManager.CurrentEnemy.IsPresentAndGet(out EnemyBase enemy))
        {
            finishedAttack = true;
            return;
        }   

        enemy.Attack(damage => {
            gameManager.GetAliveHunters()
                .ForEach(h => h.HealthSystem.Damage(damage));

            finishedAttack = true;
        });
    }

    public bool IsTurnFinished()
    {
        return finishedAttack;
    }
}
