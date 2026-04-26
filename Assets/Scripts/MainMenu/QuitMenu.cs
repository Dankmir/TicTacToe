using UnityEngine;

public class QuitMenu : Menu
{
    [SerializeField] SimpleButton btnYes;
    [SerializeField] SimpleButton btnNo;

    private void Awake()
    {
        btnYes.onClick.AddListener(OnYes);
        btnNo.onClick.AddListener(OnNo);
    }

    private void OnYes() => Application.Quit();

    private void OnNo() => PopupsManager.Instance.Close();
}
