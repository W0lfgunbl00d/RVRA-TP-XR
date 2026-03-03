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
    
    public void SetOrbitPoints(Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    public void SetVisible(bool visible)
    {
        lineRenderer.enabled = visible;
        Debug.Log($"[ORBIT] {gameObject.name} visible={visible}");
    }
}

