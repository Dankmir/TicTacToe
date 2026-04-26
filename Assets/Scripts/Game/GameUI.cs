using PrimeTween;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Space(5)]
    [SerializeField] RectTransform gameInfoRect;
    [SerializeField] RectTransform boardRect;

    [Space(5)]
    [SerializeField] TweenSettings<float> gameInfoMoveInSettings;
    [SerializeField] TweenSettings<float> boardMoveInSettings;

    [Space(5)]
    [SerializeField] PlayerUI playerXUI;
    [SerializeField] PlayerUI playerOUI;

    private void OnEnable()
    {
        gameInfoRect.anchoredPosition = new Vector2(gameInfoRect.anchoredPosition.x, gameInfoMoveInSettings.startValue);
        boardRect.anchoredPosition = new Vector2(boardRect.anchoredPosition.x, boardMoveInSettings.startValue);
    }

    public void OnGameStart()
    {
        playerXUI.SetSprite(GameController.SelectedPlayers.sprX);
        playerOUI.SetSprite(GameController.SelectedPlayers.sprO);
        
        Tween.UIAnchoredPositionY(gameInfoRect, gameInfoMoveInSettings)
            .Group(Tween.UIAnchoredPositionY(boardRect, boardMoveInSettings));
    }

    public void OnPlayerSwitch(TicTacToe.Symbol player)
    {
        if (player == TicTacToe.Symbol.X)
        {
            playerXUI.ScaleUp();
            playerOUI.ScaleDown();
        }
        else
        {
            playerXUI.ScaleDown();
            playerOUI.ScaleUp();
        }
    }

    public void OnPlayerTurnCountChanged(TicTacToe.Symbol player, int turnCount)
    {
        if (player == TicTacToe.Symbol.X)
            playerXUI.SetTurnCount(turnCount);
        else
            playerOUI.SetTurnCount(turnCount);
    }

    public void OnGameEnd()
    {
        playerXUI.ScaleUp();
        playerOUI.ScaleUp();
    }
}
