using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField] Popup popup;

    public void Open() => PopupsManager.Instance.Open(popup);
    public void Close() => PopupsManager.Instance.Close();
}
