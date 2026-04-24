using UnityEngine;
using TMPro;

public class StatsMenu : Menu
{
    [SerializeField] TextMeshProUGUI txtTotalGames;
    [SerializeField] TextMeshProUGUI txtXWins;
    [SerializeField] TextMeshProUGUI txtOWins;
    [SerializeField] TextMeshProUGUI txtTotalDraws;
    [SerializeField] TextMeshProUGUI txtAvgPlayTime;

    public void RefreshStats(Stats stats)
    {
        txtTotalGames.text = $"Total Games: {stats.totalGames}";
        txtXWins.text = $"WINS X: {stats.xWins}";
        txtOWins.text = $"WINS O: {stats.oWins}";
        txtTotalDraws.text = $"Total Draws: {stats.totalDraws}";
        txtAvgPlayTime.text = $"Avg. Play Time: {FormatTime(stats.avgPlayTime)}";
    }

    private string FormatTime(float seconds)
    {
        if (seconds < 60f)
        {
            return $"{seconds:F2}s";
        }
        else if (seconds < 3600f)
        {
            float minutes = seconds / 60f;
            return $"{minutes:F2}m";
        }
        else
        {
            float hours = seconds / 3600f;
            return $"{hours:F2}h";
        }
    }
}
