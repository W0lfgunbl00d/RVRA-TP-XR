using UnityEngine;

public class ScaleController
{
    readonly Transform target;
    readonly float minScale;
    readonly float maxScale;

    float currentScale;

    public ScaleController(Transform target, float initialScale, float minScale, float maxScale)
    {
        this.target = target;
        this.minScale = minScale;
        this.maxScale = maxScale;

        SetScale(initialScale);
        Debug.Log($"[SCALE_CTRL] Initialisé — scale={initialScale:F2}, min={minScale}, max={maxScale}");
    }

    /// <summary>
    /// Définit l'échelle (valeur absolue). Clampe automatiquement.
    /// </summary>
    public void SetScale(float value)
    {
        float clamped = Mathf.Clamp(value, minScale, maxScale);

        if (!Mathf.Approximately(clamped, value))
        {
            Debug.LogWarning($"[WARN] Scale clamped: {value:F3} → {clamped:F3} (min={minScale}, max={maxScale})");
        }

        currentScale = clamped;
        target.localScale = Vector3.one * currentScale;
        Debug.Log($"[XR] Scale applied: {currentScale:F3}");
    }

    /// <summary>
    /// Modifie l'échelle de manière relative (+/- delta).
    /// </summary>
    public void AdjustScale(float delta)
    {
        Debug.Log($"[INPUT] Scale requested: current={currentScale:F3}, delta={delta:F3}");
        SetScale(currentScale + delta);
    }

    public float CurrentScale => currentScale;
}

