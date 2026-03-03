using UnityEngine;

[CreateAssetMenu(menuName = "XR/Solar System Config")]
public class SolarSystemConfig : ScriptableObject
{
    [Header("Échelle")]
    public float distanceScale = 0.000001f;
    public float planetSizeScale = 0.01f;

    [Header("Affichage")]
    public bool showOrbits = true;

    [Header("Debug")]
    [Tooltip("Afficher le panneau de debug dans le casque")]
    public bool showDebugOverlay = true;
}
