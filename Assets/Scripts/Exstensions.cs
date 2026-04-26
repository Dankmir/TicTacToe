using UnityEngine;

public static class Exstensions
{
    public static int Mod(this int a, int m) => (a % m + m) % m;


    public static string FormatTime(this float seconds)
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
