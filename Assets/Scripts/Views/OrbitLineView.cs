using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitLineView : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
    }

    /// <summary>
    /// Set the orbit points on the LineRenderer.
    /// Call this only when the orbit needs to be recalculated (not every frame).
    /// </summary>
    public void SetOrbitPoints(Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    public void SetVisible(bool visible)
    {
        lineRenderer.enabled = visible;
    }
}

