using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlay);
        btnStats.onClick.AddListener(OnStats);
        btnStatsClose.onClick.AddListener(OnStatsClose);
        StatsManager.Instance.OnStatsChanged += statsMenu.RefreshStats;
    }

    private async void Start()
    {
        AudioManager.Instance.PlayMusic(Music.Main);
        await StatsManager.Instance.Load();
    }

    private void OnPlay()
    {
        mainMenu.Hide(() =>
        {
            btnQuit.gameObject.SetActive(false);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        });
    }

    private void OnStats()
    {
        mainMenu.Hide(() => statsMenu.Show());
    }

    private void OnStatsClose()
    {
        statsMenu.Hide(() => mainMenu.Show());
    }
}
