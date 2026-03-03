using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabLogger : MonoBehaviour
{
    void Start()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
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