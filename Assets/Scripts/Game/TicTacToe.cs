using UnityEngine;
using System;

public class TicTacToe
{
    public enum Symbol { X, O, None }
    public enum Result { InvalidMove, None, WinX, WinO, Draw }

    private readonly int size;
    private Symbol[,] board;
    private int[] rows;
    private int[] cols;
    private int diag;
    private int antiDiag;
    private Symbol currentPlayer;

    public event Action<int, int, Symbol> OnValidMove;
    public event Action<Symbol> OnPlayerSwitch;
    public event Action<Symbol> OnGameWon;
    public event Action OnDraw;
    public event Action<Symbol> OnGameEnd;

    public bool IsGameOver { get; private set; }

    public TicTacToe(int size)
    {
        this.size = size;
    }

    public void Start()
    {
        board = new Symbol[size, size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                board[i, j] = Symbol.None;

        rows = new int[size];
        cols = new int[size];
        diag = 0;
        antiDiag = 0;

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

        bool isWinner = StepBoard(row, col, currentPlayer);

        if (isWinner)
        {
            OnGameEnd?.Invoke(currentPlayer);
            IsGameOver = true;
        }
        else if (IsBoardFull())
        {
            OnGameEnd?.Invoke(Symbol.None);
            IsGameOver = true;
        }
        else
            SwitchPlayer();
    }

    private bool IsValidMove(int row, int col)
    {
        if (row >= size || row < 0 || col >= size || col < 0)
            return false;

        if (board[row, col] != Symbol.None)
            return false;

        return true;
    }

    private bool StepBoard(int row, int col, Symbol symbol)
    {
        int a = symbol == Symbol.X ? 1 : -1;

        rows[row] += a;
        cols[col] += a;

        if (row == col)
            diag += a;

        if (row + col == size - 1)
            antiDiag += a;

        return Mathf.Abs(rows[row]) == size
            || Mathf.Abs(cols[col]) == size
            || Mathf.Abs(diag) == size
            || Mathf.Abs(antiDiag) == size;
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