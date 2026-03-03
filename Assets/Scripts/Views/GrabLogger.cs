using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabLogger : MonoBehaviour
{
    void Start()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable == null)
        {
            Debug.LogError($"[XR] GrabLogger: XRGrabInteractable introuvable sur {gameObject.name} !");
            return;
        }

        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
        Debug.Log($"[XR] GrabLogger initialisé sur {gameObject.name}");
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("[XR] Table grabbed");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("[XR] Table released");
    }
}