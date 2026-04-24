using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

public class BoardSymbol : MonoBehaviour
{
    [SerializeField] Image symbolImage;

    [Space(5)]
    [SerializeField] TweenSettings<float> scaleSettings;
    [SerializeField] TweenSettings<float> alphaSettings;

    private Sequence spawnSequence;

    private void OnEnable()
    {
        transform.localScale = Vector2.one * scaleSettings.startValue;

        var col = symbolImage.color;
        col.a = alphaSettings.startValue;
        symbolImage.color = col;

        spawnSequence = Tween.Scale(transform, scaleSettings)
            .Group(Tween.Alpha(symbolImage, alphaSettings));
    }

    private void OnDisable()
    {
        spawnSequence.Stop();
    }
}
