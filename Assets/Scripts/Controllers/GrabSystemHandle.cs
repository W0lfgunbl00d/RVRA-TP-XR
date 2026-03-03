using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabSystemHandle : XRGrabInteractable
{
    [Header("Parent Transform Override")]
    [Tooltip("Assign the parent object that should move instead of this object")]
    public Transform targetParent;

    private Vector3 localPositionOffset;
    private Quaternion localRotationOffset;

    protected override void Awake()
    {
        base.Awake();

        movementType = MovementType.Instantaneous;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (targetParent == null) return;

        // Cast to IXRSelectInteractor (XRI 3.x)
        IXRSelectInteractor interactor = args.interactorObject;
        Transform interactorTransform = interactor.GetAttachTransform(this);

        // CORRECT MATH: Get the parent's position/rotation in the INTERACTOR'S local space
        localPositionOffset = interactorTransform.InverseTransformPoint(targetParent.position);
        localRotationOffset = Quaternion.Inverse(interactorTransform.rotation) * targetParent.rotation;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected || targetParent == null) return;

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            IXRSelectInteractor interactor = firstInteractorSelecting;
            Transform interactorTransform = interactor.GetAttachTransform(this);

            // CORRECT MATH: Apply the local offsets relative to the interactor's current transform
            targetParent.position = interactorTransform.TransformPoint(localPositionOffset);
            targetParent.rotation = interactorTransform.rotation * localRotationOffset;
        }
    }
}