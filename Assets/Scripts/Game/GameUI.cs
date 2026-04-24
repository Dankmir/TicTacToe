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
        Tween.UIAnchoredPositionY(gameInfoRect, gameInfoMoveInSettings)
            .Group(Tween.UIAnchoredPositionY(boardRect, boardMoveInSettings));
    }

    public void OnPlayerSwitch(TicTacToe.Symbol player)
    {
        Debug.Log("Switch: " + player.ToString());
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

    public void OnGameEnd()
    {
        playerXUI.ScaleUp();
        playerOUI.ScaleUp();
    }
}
