using UnityEngine.Events;
using UnityEngine;
using PrimeTween;

public class BoardField : ButtonBehaviour
{
    [SerializeField] RectTransform innerRect;
    [HideInInspector] public UnityEvent onClick = new();

    private Tween enterTween, exitTween;

    private bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (!_isActive)
                OnHoverStateChanged(false);
        }
    }

    public override void OnHoverStateChanged(bool isHovered)
    {
        if (isHovered && _isActive)
        {
            exitTween.Stop();
            enterTween = Tween.Scale(innerRect, innerRect.localScale, Vector2.one * 0.9f, 0.15f, ease: Ease.OutBack);
        }
        else
        {
            enterTween.Stop();
            exitTween = Tween.Scale(innerRect, innerRect.localScale, Vector2.one, 0.15f, ease: Ease.OutBack);
        }
    }

    public override void OnClick()
    {
        AudioManager.Instance.PlaySfx(Sound.BoardClick);
        onClick?.Invoke();
    }
}