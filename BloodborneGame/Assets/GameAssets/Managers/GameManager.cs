using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.GameManagers;
using Assets.UnityFoundation.TimeUtils;
using Assets.UnityFoundation.UI.Menus.GameOverMenu;
using System;
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

            if(currentEnemy.IsPresent)
            {
                currentEnemy.Get().HealthSystem.OnDied += (sender, args) => {
                    enemyDiscart.Discart(currentEnemy.Get());
                    currentEnemy = Optional<EnemyBase>.None();
               };
            }
        }
    }

    private List<Hunter> hunters = new List<Hunter>();

    public List<Hunter> GetAliveHunters() =>  hunters.FindAll(h => !h.IsDead);

    private ITurn currentTurn;

    [SerializeField] private GameOverMenu gameOverMenu;
    private GameCanvasUI gameCanvas;
    private EnemyDeckManager enemyDeck;
    private EnemyDiscartPileManager enemyDiscart;

    private bool canChangeTurn = false;
    private int currentRound = 0;

    private void Start()
    {
        gameCanvas = GameCanvasUI.Instance;
        enemyDeck = EnemyDeckManager.Instance;
        enemyDiscart = EnemyDiscartPileManager.Instance;

        gameOverMenu.Setup(
            "Retry",
            () => StartCoroutine(nameof(GameBoostrap))
        );

        StartCoroutine(nameof(GameBoostrap));
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
            gameCanvas.UpdateRound(++currentRound);

            StartCoroutine(ChangeTurn(new HuntersChooseCardsTurn(hunters)));
            return;
        }

        if(currentTurn.GetType() == typeof(HuntersChooseCardsTurn))
        {
            StartCoroutine(ChangeTurn(new ImmediateEffectTurn(CurrentEnemy, hunters)));
            return;
        }

        if(currentTurn.GetType() == typeof(ImmediateEffectTurn))
        {
            StartCoroutine(ChangeTurn(new MonsterAttackTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(MonsterAttackTurn))
        {
            StartCoroutine(ChangeTurn(new HuntersAttackTurn(CurrentEnemy, hunters)));
            return;
        }

        if(currentTurn.GetType() == typeof(HuntersAttackTurn))
        {
            StartCoroutine(ChangeTurn(new MonsterEscapeTurn(this)));
            return;
        }

        if(currentTurn.GetType() == typeof(MonsterEscapeTurn))
        {
            StartCoroutine(ChangeTurn(new RevealMonsterTurn(this, hunters)));
        }
    }

    private IEnumerator GameBoostrap()
    {
        gameCanvas.ChangeTurn("Creating game...");

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
        gameCanvas.ChangeTurn("Starting game...");

        StartCoroutine(ChangeTurn(new RevealMonsterTurn(this, hunters)));

        currentRound = 0;
    }

    private IEnumerator ChangeTurn(ITurn turn)
    {
        canChangeTurn = false;
        gameCanvas.ChangeTurn(turn.GetType().ToString());
        currentTurn = turn;

        yield return new WaitForSeconds(2f);

        currentTurn.Execute();

        yield return new WaitForSeconds(2f);

        canChangeTurn = true;
    }
}
