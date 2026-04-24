using UnityEngine.Events;
using UnityEngine;
using PrimeTween;

public class ToggleButton : ButtonBehaviour
{
    [Header("States")]
    [SerializeField] GameObject onState;
    [SerializeField] GameObject offState;

    [Space(5)]
    [SerializeField] ShakeSettings clickSettings;

    [Space(5)]
    public UnityEvent<bool> onToggle = new();

    private bool _toggleState = true;
    public bool ToggleState
    {
        get => _toggleState;
        set
        {
            _toggleState = value;
            onToggle?.Invoke(value);
            onState.SetActive(_toggleState);
            offState.SetActive(!_toggleState);
        }
    }
    public override void OnClick()
    {
        ToggleState = !ToggleState;
        AudioManager.Instance.PlaySfx(Sound.Click);
        Tween.PunchScale(transform, clickSettings);
    }
}
