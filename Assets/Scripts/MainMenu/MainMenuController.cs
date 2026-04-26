using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] SimpleButton btnQuit;

    [Header("Main Menu")]
    [SerializeField] MainMenu mainMenu;
    [SerializeField] SimpleButton btnPlay;
    [SerializeField] SimpleButton btnStats;

    [Header("Stats Menu")]
    [SerializeField] StatsMenu statsMenu;
    [SerializeField] SimpleButton btnStatsClose;

    [Header("Theme Selection Menu")]
    [SerializeField] ThemeSelectionMenu themeSelectionMenu;
    [SerializeField] SimpleButton btnStart;
    [SerializeField] SimpleButton btnBack;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlay);
        btnStats.onClick.AddListener(OnStats);
        btnStatsClose.onClick.AddListener(OnStatsClose);
        btnStart.onClick.AddListener(OnStart);
        btnBack.onClick.AddListener(OnBack);

        StatsManager.Instance.OnStatsChanged += statsMenu.RefreshStats;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(Music.Main);
        StatsManager.Instance.LoadStats();

        mainMenu.Show();
    }

    private void OnPlay() => mainMenu.Hide(themeSelectionMenu.Show);

    private void OnStart()
    {
        themeSelectionMenu.Hide(() =>
        {
            btnQuit.gameObject.SetActive(false);
            GameController.SelectedPlayers = themeSelectionMenu.GetSelectedPlayers();
            SceneManager.LoadScene("GameScene");
        });
    }

    private void OnBack() => themeSelectionMenu.Hide(mainMenu.Show);

    private void OnStats() => mainMenu.Hide(statsMenu.Show);

    private void OnStatsClose() => statsMenu.Hide(mainMenu.Show);

    private void OnDestroy()
    {
        // StatsManager.Instance.OnStatsChanged -= statsMenu.RefreshStats;
    }
}
