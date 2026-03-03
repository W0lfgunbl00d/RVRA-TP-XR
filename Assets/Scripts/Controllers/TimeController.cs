using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Tooltip("Nombre de jours simulés par seconde réelle")]
    public float secondsPerDay = 30f;

    TimeModel model;
    DateTime current;

    public void Init(TimeModel m)
    {
        model = m;
        current = DateTime.Now;
        model.SetTime(current);
        Debug.Log($"[TIME] Init — date de départ : {current:yyyy-MM-dd HH:mm}");
    }

    void Update()
    {
        if (model == null) return;
        if (!model.IsPlaying) return;

        current = current.AddDays(Time.deltaTime * secondsPerDay);
        model.SetTime(current);
    }
}
