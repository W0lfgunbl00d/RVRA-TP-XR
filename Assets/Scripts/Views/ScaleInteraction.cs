using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Lit les boutons B (zoom) et A (dézoom) du contrôleur droit pour changer l'échelle.
/// Ne manipule JAMAIS le transform directement — appelle ScaleController.
/// Update() justifié : lecture continue des boutons maintenus.
/// Initialisé par AppBootstrapper via Init().
/// </summary>
public class ScaleInteraction : MonoBehaviour
{
    [Header("Input")]
    [Tooltip("Vitesse de changement d'échelle par seconde")]
    [SerializeField] float scaleSpeed = 0.3f;

    ScaleController scaleController;

    InputAction zoomInAction;   // zoom in
    InputAction zoomOutAction;  // zoom out

    public void Init(ScaleController controller)
    {
        scaleController = controller;

        // Bouton B (primaryButton sur le contrôleur droit) → zoom in
        zoomInAction = new InputAction("ZoomIn", InputActionType.Button);
        zoomInAction.AddBinding("<XRController>{LeftHand}/primaryButton");
        zoomInAction.AddBinding("<Keyboard>/pageUp");
        zoomInAction.Enable();

        // Bouton A (secondaryButton sur le contrôleur droit) → zoom out
        zoomOutAction = new InputAction("ZoomOut", InputActionType.Button);
        zoomOutAction.AddBinding("<XRController>{LeftHand}/secondaryButton");
        zoomOutAction.AddBinding("<Keyboard>/pageDown");
        zoomOutAction.Enable();

        Debug.Log("[SCALE_INPUT] Initialisé (Y=zoom in, X=zoom out / PageUp-PageDown)");
    }

    void OnDestroy()
    {
        zoomInAction?.Disable();
        zoomInAction?.Dispose();
        zoomOutAction?.Disable();
        zoomOutAction?.Dispose();
    }

    // Update() justifié : lecture continue des boutons maintenus
    void Update()
    {
        if (scaleController == null) return;

        float delta = 0f;

        if (zoomInAction.IsPressed())
            delta += scaleSpeed * Time.deltaTime;

        if (zoomOutAction.IsPressed())
            delta -= scaleSpeed * Time.deltaTime;

        if (!Mathf.Approximately(delta, 0f))
            scaleController.AdjustScale(delta);
    }
}
