using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameUI gameUI;
    [SerializeField] GameTimer gameTimer;
    [SerializeField] BoardField[] fields;
    [SerializeField] GameObject symbolX;
    [SerializeField] GameObject symbolO;

    private readonly List<GameObject> spawnedSymbols = new();

    private TicTacToe ticTacToe;
    private bool turnInProgress;

    private readonly int boardSize = 3;

    private void Start()
    {
        gameUI.OnGameStart();
        Initialize();
        gameTimer.StartTimer();
    }

    private void Initialize()
    {
        ticTacToe = new(boardSize);
        for (int i = 0; i < fields.Length; i++)
        {
            int row = i % boardSize;
            int col = i / boardSize;
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

        ticTacToe?.Start();
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

        var field = fields[col * boardSize + row];
        field.IsActive = false;

        InstantiateSymbol(symbol, field.transform as RectTransform);

        turnInProgress = false;
    }

    private void InstantiateSymbol(TicTacToe.Symbol symbol, RectTransform fieldRect)
    {
        var obj = Instantiate(symbol == TicTacToe.Symbol.X ? symbolX : symbolO, fieldRect);
        spawnedSymbols.Add(obj);
    }

    private void OnGameEnd(TicTacToe.Symbol winner)
    {
        gameTimer.StopTimer();
        gameUI.OnGameEnd();
        UpdateStats(winner);
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
        StatsManager.Instance.Save();
    }

    private void OnDestroy()
    {
        ticTacToe.OnValidMove -= OnValidMove;
        ticTacToe.OnPlayerSwitch -= gameUI.OnPlayerSwitch;
        ticTacToe.OnGameEnd -= OnGameEnd;
    }
}
