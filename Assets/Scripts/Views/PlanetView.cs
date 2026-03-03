using UnityEngine;

public class PlanetView : MonoBehaviour
{
    [Tooltip("Type de planète — assigné via l'inspecteur")]
    public PlanetData.Planet planet;

    [Tooltip("Optional OrbitLineView to display the orbital trajectory")]
    public OrbitLineView orbitLine;

    public void SetPosition(Vector3 pos)
    {
        // Si un Rigidbody est présent et non-kinematic, il écrase localPosition
        var rb = GetComponent<Rigidbody>();
        if (rb != null && !rb.isKinematic)
        {
            Debug.LogWarning($"[PLANET_VIEW] {planet} a un Rigidbody non-kinematic ! Le mouvement sera bloqué.");
        }

        transform.localPosition = pos;
    }
}
