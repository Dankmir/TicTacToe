using UnityEngine;

public class VolumeCross : MonoBehaviour
{
    public void OnMuted(bool isMuted) => gameObject.SetActive(!isMuted);
}
