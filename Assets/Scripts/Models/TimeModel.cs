using System;
using UnityEngine;

public class TimeModel
{
    public DateTime CurrentTime { get; private set; }

    public float TimeScale { get; private set; } = 1f;

    public bool IsPlaying { get; private set; } = true;

    public event Action<DateTime> OnTimeChanged;

    public void SetTime(DateTime t)
    {
        CurrentTime = t;
        OnTimeChanged?.Invoke(CurrentTime);
    }

    public void SetScale(float scale)
    {
        TimeScale = scale;
        Debug.Log($"[TIME_MODEL] TimeScale changé : {scale}");
    }

    public void Play()
    {
        IsPlaying = true;
        Debug.Log("[TIME_MODEL] Play");
    }

    public void Pause()
    {
        IsPlaying = false;
        Debug.Log("[TIME_MODEL] Pause");
    }
}
