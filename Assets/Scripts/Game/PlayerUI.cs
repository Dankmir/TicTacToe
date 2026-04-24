using UnityEngine.UI;
using UnityEngine;
using PrimeTween;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image playerSymbolImage;
    [SerializeField] TextMeshProUGUI txtTurnCount;

    [Space(5)]
    [SerializeField] TweenSettings<float> scaleUpSettings;
    [SerializeField] TweenSettings<float> scaleDownSettings;

    private Tween scaleUpTween, scaleDownTween;

    public void ScaleUp()
    {
        if (transform.localScale.x == scaleUpSettings.endValue)
            return;

        scaleDownTween.Stop();
        scaleUpTween = Tween.Scale(transform, scaleUpSettings);
    }

    public void ScaleDown()
    {
        if (transform.localScale.x == scaleDownSettings.endValue)
            return;

        scaleUpTween.Stop();
        scaleDownTween = Tween.Scale(transform, scaleDownSettings);
    }
}
