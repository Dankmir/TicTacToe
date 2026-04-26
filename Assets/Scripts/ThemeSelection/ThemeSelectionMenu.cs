using UnityEngine;

public class ThemeSelectionMenu : Menu
{
    [SerializeField] PlayerSelection selectionX;
    [SerializeField] PlayerSelection selectionO;

    public (Sprite, Sprite) GetSelectedPlayers() => (selectionX.SelectedSprite, selectionO.SelectedSprite);
}
