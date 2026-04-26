using UnityEngine;
using System;

public class TicTacToe
{
    public enum Symbol { X, O, None }

    private readonly Symbol[,] board;
    private Symbol currentPlayer;

    public event Action<int, int, Symbol> OnValidMove;
    public event Action<Symbol> OnPlayerSwitch;
    public event Action<(Symbol winner, (int row, int col)[] line)> OnGameEnd;

    public bool IsGameOver { get; private set; }

    readonly (int row, int col)[][] winningLines = new (int, int)[][]
    {
        // Rows
        new[] { (0,0), (0,1), (0,2) },
        new[] { (1,0), (1,1), (1,2) },
        new[] { (2,0), (2,1), (2,2) },

        // Columns
        new[] { (0,0), (1,0), (2,0) },
        new[] { (0,1), (1,1), (2,1) },
        new[] { (0,2), (1,2), (2,2) },

        // Diagonals
        new[] { (0,0), (1,1), (2,2) },
        new[] { (0,2), (1,1), (2,0) }
    };

    public TicTacToe()
    {
        board = new Symbol[3, 3];
    }

    public void Start()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = Symbol.None;

        IsGameOver = false;
        currentPlayer = Symbol.X;
        OnPlayerSwitch?.Invoke(currentPlayer);
    }

    public void MakeMove(int row, int col)
    {
        if (IsGameOver || !IsValidMove(row, col))
            return;

        board[row, col] = currentPlayer;
        OnValidMove?.Invoke(row, col, currentPlayer);

        var (winner, line) = CheckWinner();

        if (winner != Symbol.None || IsBoardFull())
        {
            OnGameEnd?.Invoke((winner, line));
            IsGameOver = true;
        }
        // else if (IsBoardFull())
        // {
        //     OnGameEnd?.Invoke(());
        //     IsGameOver = true;
        // }
        else
            SwitchPlayer();
    }

    private bool IsValidMove(int row, int col)
    {
        if (row >= 3 || row < 0 || col >= 3 || col < 0)
            return false;

        if (board[row, col] != Symbol.None)
            return false;

        return true;
    }

    private (Symbol Winner, (int row, int col)[] Line) CheckWinner()
    {
        foreach (var line in winningLines)
        {
            var first = board[line[0].row, line[0].col];

            if (first == Symbol.None)
                continue;

            if (board[line[1].row, line[1].col] == first && board[line[2].row, line[2].col] == first)
                return (first, line);
        }
    
        return (Symbol.None, Array.Empty<(int, int)>());
    }

    private bool IsBoardFull()
    {
        foreach (var field in board)
            if (field == Symbol.None)
                return false;
        return true;
    }

    private void SwitchPlayer()
    {
        currentPlayer = currentPlayer == Symbol.X ? Symbol.O : Symbol.X;
        OnPlayerSwitch?.Invoke(currentPlayer);
    }
}