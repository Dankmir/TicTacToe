using UnityEngine;
public class CanvasCameraFinder : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    void OnEnable()
    {
        if (canvas)
            canvas.worldCamera = Camera.main;
    }
}
