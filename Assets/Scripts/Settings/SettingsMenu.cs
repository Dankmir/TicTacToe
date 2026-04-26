using UnityEngine;

public class SettingsMenu : Menu
{
    [SerializeField] ToggleButton musicToggle;
    [SerializeField] ToggleButton sfxToggle;

    private void OnEnable()
    {
        musicToggle.ToggleState = !AudioManager.Instance.IsMusicMuted;
        sfxToggle.ToggleState = !AudioManager.Instance.IsSFXMuted;
    }

    private void Awake()
    {
        musicToggle.onToggle.AddListener(ToggleMuteMusic);
        sfxToggle.onToggle.AddListener(ToggleMuteSFX);
    }

    private void ToggleMuteMusic(bool toggleState)
        => AudioManager.Instance.ToggleMuteMusic(!toggleState);


    private void ToggleMuteSFX(bool toggleState)
        => AudioManager.Instance.ToggleMuteSFX(!toggleState);
}
