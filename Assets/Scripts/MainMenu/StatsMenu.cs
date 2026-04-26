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
        txtAvgPlayTime.text = $"Avg. Play Time: {stats.avgPlayTime.FormatTime()}";
    }
}
