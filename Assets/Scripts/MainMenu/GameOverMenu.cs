using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameOverMenu : Menu
{
    [SerializeField] TextMeshProUGUI txtResult;
    [SerializeField] TextMeshProUGUI txtGameDuration;
    [SerializeField] SimpleButton btnExit;
    [SerializeField] SimpleButton btnRetry;


    public static TicTacToe.Symbol winner;
    public static float gameDuration;

    private void OnEnable()
    {
        if (winner == TicTacToe.Symbol.None)
            txtResult.text = "Draw";
        else
            txtResult.text = $"The winner is {winner}";

        txtGameDuration.text = $"Game duration: {gameDuration.FormatTime()}";

        AudioManager.Instance.PlaySfx(Sound.Success);
    }

    private void Start()
    {
        btnExit.onClick.AddListener(OnExit);
        btnRetry.onClick.AddListener(OnRetry);
    }

    private void OnExit()
    {
        PopupsManager.Instance.Close();
        SceneManager.LoadScene("MenuScene");
    }

    private void OnRetry()
    {
        PopupsManager.Instance.Close();
        var gameController = FindAnyObjectByType<GameController>();
        if (gameController)
            gameController.RestartGame();
    }
}
