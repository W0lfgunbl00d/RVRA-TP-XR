using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float secondsPerDay = 30f;

    TimeModel model;
    DateTime current;

    public void Init(TimeModel m)
    {
        model = m;
        current = DateTime.Now;
        model.SetTime(current);
    }

    void Update()
    {
        if (!model.IsPlaying) return;

        current = current.AddDays(Time.deltaTime * secondsPerDay);
        model.SetTime(current);
        // Debug.Log($"[TIME] Current time: {current}");
    }
}
