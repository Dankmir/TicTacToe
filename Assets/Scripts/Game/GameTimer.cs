using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtTimer;

    private readonly WaitForSeconds waitForSecond = new(1);
    private DateTime startTime;
    private Coroutine coroutine;
    private TimeSpan elapsedTime;

    public TimeSpan ElapsedTime
    {
        get => elapsedTime;
        private set
        {
            elapsedTime = value;
            txtTimer.text = elapsedTime.ToString(@"mm\:ss");
        }
    }

    public void StartTimer()
    {
        ResetTimer();
        coroutine ??= StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        if (coroutine != null)
        {
            ElapsedTime = DateTime.Now - startTime;
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void ResetTimer()
    {
        startTime = DateTime.Now;
        ElapsedTime = TimeSpan.Zero;
    }

    private IEnumerator TimerCoroutine()
    {
        startTime = DateTime.Now;
        while (true)
        {
            yield return waitForSecond;
            ElapsedTime = DateTime.Now - startTime;
        }
    }
}
