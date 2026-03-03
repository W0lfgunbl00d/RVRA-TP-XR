using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOverlay : MonoBehaviour
{
    [Header("UI References (assignées via l'inspecteur)")]
    [SerializeField] Text debugText;

    [Header("Settings")]
    [SerializeField] int maxLines = 20;

    readonly Queue<string> lines = new Queue<string>();

    bool isVisible = true;
    
    public void Init(TimeModel timeModel)
    {
        if (debugText == null)
        {
            Debug.LogWarning("[DEBUG_OVERLAY] debugText non assigné dans l'inspecteur — overlay désactivé");
            return;
        }

        timeModel.OnTimeChanged += OnTimeChanged;

        Log("[DEBUG_OVERLAY] Initialisé");
    }

    void OnTimeChanged(DateTime time)
    {
        if (debugText == null) return;
        UpdateHeader(time);
    }

    void UpdateHeader(DateTime time)
    {
        string header = $"[Simulation] {time:yyyy-MM-dd HH:mm}\nFPS: {(1f / Time.unscaledDeltaTime):F0}";
        string body = string.Join("\n", lines);
        debugText.text = $"{header}\n---\n{body}";
    }
    
    public void Log(string message)
    {
        Debug.Log(message);

        lines.Enqueue($"{DateTime.Now:HH:mm:ss} {message}");
        while (lines.Count > maxLines)
            lines.Dequeue();
    }

    public void SetVisible(bool visible)
    {
        isVisible = visible;
        if (debugText != null)
            debugText.transform.parent.gameObject.SetActive(visible);

        Debug.Log($"[DEBUG_OVERLAY] Visible: {visible}");
    }

    public bool IsVisible => isVisible;
}

