using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] GameUI gameUI;
    [SerializeField] GameTimer gameTimer;
    [SerializeField] BoardField[] fields;
    [SerializeField] BoardSymbol boardSymbol;
    [SerializeField] GameObject strikePrefab;

    public static (Sprite sprX, Sprite sprO) SelectedPlayers;

    private readonly List<GameObject> spawnedSymbols = new();

    private TicTacToe ticTacToe;
    private bool turnInProgress;

    private int turnCountX;
    private int turnCountO;


    private void Start()
    {
        gameUI.OnGameStart();
        Initialize();
        gameTimer.StartTimer();
    }

    private void Initialize()
    {
        ticTacToe = new();
        for (int i = 0; i < fields.Length; i++)
        {
            int row = i % 3;
            int col = i / 3;
            var field = fields[i];
            fields[i].onClick.AddListener(() => OnFieldClicked(row, col));
        }

        ticTacToe.OnValidMove += OnValidMove;
        ticTacToe.OnPlayerSwitch += gameUI.OnPlayerSwitch;
        ticTacToe.OnGameEnd += OnGameEnd;

        ticTacToe.Start();
    }

    public void RestartGame()
    {
        CleanupSpawnedSymbols();

        foreach (var field in fields)
            field.IsActive = true;

        gameUI.OnPlayerTurnCountChanged(TicTacToe.Symbol.X, turnCountX = 0);
        gameUI.OnPlayerTurnCountChanged(TicTacToe.Symbol.O, turnCountO = 0);

        ticTacToe?.Start();
        gameTimer.StartTimer();
    }

    private void CleanupSpawnedSymbols()
    {
        foreach (var symbol in spawnedSymbols)
            Destroy(symbol);
        spawnedSymbols.Clear();
    }

    private void OnFieldClicked(int row, int col)
    {
        if (turnInProgress)
            return;

        ticTacToe.MakeMove(row, col);
    }

    private void OnValidMove(int row, int col, TicTacToe.Symbol symbol)
    {
        turnInProgress = true;

        int turnCount = symbol == TicTacToe.Symbol.X ? ++turnCountX : ++turnCountO;
        gameUI.OnPlayerTurnCountChanged(symbol, turnCount);

        var field = fields[col * 3 + row];
        field.IsActive = false;

        InstantiateSymbol(symbol, field.transform as RectTransform);

        turnInProgress = false;
    }

    private void InstantiateSymbol(TicTacToe.Symbol symbol, RectTransform fieldRect)
    {
        var obj = Instantiate(boardSymbol, fieldRect);
        obj.SetSprite(symbol == TicTacToe.Symbol.X ? SelectedPlayers.sprX : SelectedPlayers.sprO);
        spawnedSymbols.Add(obj.gameObject);
    }

    private void OnGameEnd((TicTacToe.Symbol winner, (int row, int col)[] line) result)
    {
        gameTimer.StopTimer();
        gameUI.OnGameEnd();
        UpdateStats(result.winner);

        var lineTransforms = result.line.Select(x => fields[x.col * 3 + x.row].transform).ToArray();
        StartCoroutine(GameOverCoroutine(result.winner, lineTransforms));
    }

    private IEnumerator GameOverCoroutine(TicTacToe.Symbol winner, Transform[] line)
    {
        if (winner != TicTacToe.Symbol.None)
        {
            var dir = (line[0].position - line[1].position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            var strike = Instantiate(strikePrefab, line[1]).transform;
            strike.SetParent(line[1].parent);
            strike.SetAsLastSibling();
            strike.rotation = Quaternion.Euler(0, 0, angle);
            spawnedSymbols.Add(strike.gameObject);

            yield return new WaitForSeconds(0.5f);;
        }

        yield return new WaitForSeconds(1);;
        OpenGameOverPopup(winner, (float)gameTimer.ElapsedTime.TotalSeconds);
    }

    private void UpdateStats(TicTacToe.Symbol winner)
    {
        StatsManager.Instance.Stats.totalGames++;

        if (winner == TicTacToe.Symbol.X)
            StatsManager.Instance.Stats.xWins++;
        else if (winner == TicTacToe.Symbol.O)
            StatsManager.Instance.Stats.oWins++;
        else
            StatsManager.Instance.Stats.totalDraws++;

        float currentAvgTime = StatsManager.Instance.Stats.avgPlayTime;
        long totalGames = StatsManager.Instance.Stats.totalGames;
        float newAverage = currentAvgTime + ((float)gameTimer.ElapsedTime.TotalSeconds - currentAvgTime) / totalGames;

        StatsManager.Instance.Stats.avgPlayTime = newAverage;
        StatsManager.Instance.SaveStats();
    }

    private void OpenGameOverPopup(TicTacToe.Symbol winner, float duration)
    {
        GameOverMenu.winner = winner;
        GameOverMenu.gameDuration = duration;
        PopupsManager.Instance.Open(Popup.GameOver);
    }

    private void OnDestroy()
    {
        ticTacToe.OnValidMove -= OnValidMove;
        ticTacToe.OnPlayerSwitch -= gameUI.OnPlayerSwitch;
        ticTacToe.OnGameEnd -= OnGameEnd;

        SelectedPlayers = (null, null);
    }
}
