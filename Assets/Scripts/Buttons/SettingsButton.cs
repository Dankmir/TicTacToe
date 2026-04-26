using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] Menu settingsMenu;
    [SerializeField] SimpleButton settingsButton;

    private void Awake()
    {
        settingsButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (settingsMenu.IsShown)
            settingsMenu.Hide();
        else
            settingsMenu.Show();
    }
}
