using UnityEngine;
using System.IO;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public event Action<Stats> OnStatsChanged;

    private Stats _stats;
    public Stats Stats
    {
        get => _stats;
        set => OnStatsChanged?.Invoke(_stats = value);
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void LoadStats()
    {
        var data = PlayerPrefs.GetString("stats", "{}");
        Stats = JsonUtility.FromJson<Stats>(data);
    }

    public void SaveStats()
    {
        var json = JsonUtility.ToJson(Stats);
        PlayerPrefs.SetString("stats", json);
    }

    public async void Load()
    {
        var path = Path.Join(Application.persistentDataPath, "stats.json");
        if (!File.Exists(path))
        {
            Stats = new();
            return;
        }

        try
        {
            var dataString = await File.ReadAllTextAsync(path, Application.exitCancellationToken);
            Stats = JsonUtility.FromJson<Stats>(dataString);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public async void Save()
    {
        try
        {
            var path = Path.Join(Application.persistentDataPath, "stats.json");
            var json = JsonUtility.ToJson(Stats);
            await File.WriteAllTextAsync(path, json);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}