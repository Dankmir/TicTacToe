using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

public class SimpleButton : ButtonBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite hoverSprite;

    [Space(5)]
    [SerializeField] ShakeSettings clickSettings;

    [Space(5)]
    public UnityEvent onClick = new();


    public override void OnClick()
    {
        onClick?.Invoke();
        AudioManager.Instance.PlaySfx(Sound.Click);
        Tween.PunchScale(transform, clickSettings);
    }

    public override void OnHoverStateChanged(bool isHovered)
    {
        if (buttonImage && hoverSprite)
            buttonImage.overrideSprite = isHovered ? hoverSprite : null;
    }
}
