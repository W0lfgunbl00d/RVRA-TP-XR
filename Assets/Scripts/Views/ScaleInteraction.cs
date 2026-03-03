using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Lit le thumbstick droit (Y axis) du contrôleur VR pour zoomer/dézoomer.
/// Ne manipule JAMAIS le transform directement — appelle ScaleController.
/// Update() justifié : lecture continue d'un axe analogique.
/// Initialisé par AppBootstrapper via Init().
/// </summary>
public class ScaleInteraction : MonoBehaviour
{
    [Header("Input")]
    [Tooltip("Vitesse de changement d'échelle par seconde")]
    [SerializeField] float scaleSpeed = 0.3f;

    ScaleController scaleController;

    InputAction thumbstickAction;

    public void Init(ScaleController controller)
    {
        scaleController = controller;

        // Crée une action qui lit le thumbstick droit (axe Y) sur le contrôleur VR
        thumbstickAction = new InputAction("ScaleThumbstick", InputActionType.Value);
        thumbstickAction.AddBinding("<XRController>{RightHand}/thumbstick/y");
        // Fallback clavier pour les tests sans casque
        thumbstickAction.AddCompositeBinding("1DAxis")
            .With("Positive", "<Keyboard>/pageUp")
            .With("Negative", "<Keyboard>/pageDown");
        thumbstickAction.Enable();

        Debug.Log("[SCALE_INPUT] Initialisé (thumbstick droit Y / PageUp-PageDown)");
    }

    void OnDestroy()
    {
        thumbstickAction?.Disable();
        thumbstickAction?.Dispose();
    }

    // Update() justifié : lecture continue d'un axe analogique (thumbstick)
    void Update()
    {
        if (scaleController == null || thumbstickAction == null) return;

        float input = thumbstickAction.ReadValue<float>();

        // Dead zone pour éviter les micro-mouvements
        if (Mathf.Abs(input) < 0.15f) return;

        scaleController.AdjustScale(input * scaleSpeed * Time.deltaTime);
    }
}

