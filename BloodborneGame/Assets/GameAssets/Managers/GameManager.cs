using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Code.TimeUtils;
using Assets.UnityFoundation.GameManagers;
using Assets.UnityFoundation.TimeUtils;
using Assets.UnityFoundation.UI.Menus.GameOverMenu;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : BaseGameManager
{
    private Optional<EnemyBase> currentEnemy;
    public Optional<EnemyBase> CurrentEnemy {
        get => currentEnemy;
        set {
            currentEnemy = value;

            if(currentEnemy.IsPresentAndGet(out EnemyBase enemy))
            {
                enemy.HealthSystem.OnDied += (sender, args) => {
                    enemyDiscart.Discart(enemy);
                    Destroy(enemy.gameObject);
                    currentEnemy = Optional<EnemyBase>.None();
               };
            }
        }
    }

    private List<Hunter> hunters = new List<Hunter>();

    public List<Hunter> GetAliveHunters() =>  hunters.FindAll(h => !h.IsDead);

    private ITurn currentTurn;

    [SerializeField] private GameOverMenu gameOverMenu;
    private EnemyDeckManager enemyDeck;
    private EnemyDiscartPileManager enemyDiscart;

    private bool canChangeTurn = false;
    private int currentRound = 0;

    private float startTime;
    private Timer updateTimeCanvasTimer;

    private void Start()
    {
        enemyDeck = EnemyDeckManager.Instance;
        enemyDiscart = EnemyDiscartPileManager.Instance;

        gameOverMenu.Setup(
            "Retry",
            () => StartCoroutine(GameBoostrap())
        );

        StartCoroutine(GameBoostrap());
    }
    
    private void Update()
    {
        if(!canChangeTurn) return;

        if(GetAliveHunters().Count == 0)
        {
            gameOverMenu.Show("You Died");
            return;
        }

        if(!currentTurn.IsTurnFinished())
            return;

        if(currentTurn.GetType() == typeof(RevealMonsterTurn))
        {
            GameTurnsUI.Instance.UpdateRoundCounter(++currentRound);
            GameTurnsUI.Instance.ChangeTurn(1);

            StartCoroutine(ChangeTurn(new HuntersChooseCardsTurn(hunters)));
            return;
        }

        if(currentTurn.GetType() == typeof(HuntersChooseCardsTurn))
        {
            GameTurnsUI.Instance.ChangeTurn(2);
            StartCoroutine(ChangeTurn(new ImmediateEffectTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(ImmediateEffectTurn))
        {
            GameTurnsUI.Instance.ChangeTurn(3);
            StartCoroutine(ChangeTurn(new MonsterAttackTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(MonsterAttackTurn))
        {
            GameTurnsUI.Instance.ChangeTurn(4);
            StartCoroutine(ChangeTurn(new HuntersAttackTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(HuntersAttackTurn))
        {
            GameTurnsUI.Instance.ChangeTurn(5);
            StartCoroutine(ChangeTurn(new MonsterEscapeTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(MonsterEscapeTurn))
        {
            GameTurnsUI.Instance.ChangeTurn(6);
            StartCoroutine(
                ChangeTurn(new HuntersDreamTurn(this, HuntersDreamUI.Instance)
            ));
            return;
        }

        if(currentTurn.GetType() == typeof(HuntersDreamTurn)){
            GameTurnsUI.Instance.ChangeTurn(0);
            StartCoroutine(ChangeTurn(new RevealMonsterTurn(this, hunters)));
        }
    }

    private IEnumerator GameBoostrap()
    {
        if(DebugMode)
            Debug.Log("Creating game...");

        enemyDeck.Setup();

        yield return WaittingCoroutine.UntilPropertyTrue(
            enemyDeck, nameof(enemyDeck.IsDeckSetupFinished)
        );

        CurrentEnemy = Optional<EnemyBase>.None();

        hunters = GameObject.FindGameObjectsWithTag(Tags.hunter)
            .Select(go => go.GetComponent<Hunter>())
            .ToList();

        StartGame();
    }

    private void StartGame()
    {
        if(DebugMode)
            Debug.Log("Starting game...");

        startTime = Time.time;

        updateTimeCanvasTimer = new Timer(
            1f, 
            () => GameTurnsUI.Instance.UpdateTimeCounter(Time.time - startTime)
        )
        .Start();

        GameTurnsUI.Instance.ChangeTurn(0);
        StartCoroutine(ChangeTurn(new RevealMonsterTurn(this, hunters)));

        currentRound = 0;
    }

    private IEnumerator ChangeTurn(ITurn turn)
    {
        canChangeTurn = false;

        if(DebugMode)
            Debug.Log(turn.GetType().ToString());

        currentTurn = turn;

        yield return new WaitForSeconds(2f);

        currentTurn.Execute();

        yield return new WaitForSeconds(2f);

        canChangeTurn = true;
    }
}
