using UnityEngine;

public class PlanetView : MonoBehaviour
{
    public PlanetData.Planet planet;

    [Tooltip("Optional OrbitLineView to display the orbital trajectory")]
    public OrbitLineView orbitLine;

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }
}
