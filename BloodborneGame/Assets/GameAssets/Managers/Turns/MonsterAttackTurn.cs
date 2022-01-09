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
            gameManager
                .GetAliveHunters()
                .ForEach(h => h.HealthSystem.Damage(EvaluateDamage(h, damage)));

            finishedAttack = true;
        });
    }

    public int EvaluateDamage(Hunter h, int damage)
    {
        if(h.CurrentCard.IsPresentAndGet(out HunterCardSO card))
        {
            if(card.effect is IDefenseEffect defenseCard)
                return (int)defenseCard.Evaluate(damage);
        }

        return damage;
    }

    public bool IsTurnFinished()
    {
        return finishedAttack;
    }
}
