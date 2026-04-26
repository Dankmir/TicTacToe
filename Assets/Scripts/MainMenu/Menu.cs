using UnityEngine;
using PrimeTween;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    [Space(5)]
    [SerializeField] TweenSettings<float> showSettings;
    [SerializeField] TweenSettings<float> hideSettings;

    public bool IsShown { get; private set; }

    private Tween showTween, hideTween;

    public void Show() => Show(null);
    public void Hide() => Hide(null);

    public void Show(Action onShown)
    {
        hideTween.Stop();
        gameObject.SetActive(true);
        showTween = Tween.UIAnchoredPositionY(rect, showSettings).OnComplete(onShown);
        AudioManager.Instance.PlaySfx(Sound.PopupOpen);
        IsShown = true;
    }

    public void Hide(Action onHidden)
    {
        showTween.Stop();
        hideTween = Tween.UIAnchoredPositionY(rect, hideSettings).OnComplete(() =>
        {
            gameObject.SetActive(false);
            onHidden?.Invoke();
            IsShown = false;
        });
    }
}
