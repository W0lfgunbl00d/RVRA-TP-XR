using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(Collider))]
public class PlanetSelectable : XRSimpleInteractable
{
    PlanetView planetView;
    PlanetSelectionModel selectionModel;

    protected override void Awake()
    {
        base.Awake();
    }
    
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return false;
    }

    public void Init(PlanetView view, PlanetSelectionModel model)
    {
        planetView = view;
        selectionModel = model;
        Debug.Log($"[SELECTABLE] {view.planet} prêt à être sélectionné");
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (selectionModel == null || planetView == null) return;

        selectionModel.Select(planetView);
        Debug.Log($"[XR] Planète pointée : {planetView.planet}");
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (selectionModel == null) return;

        selectionModel.Deselect();
        Debug.Log($"[XR] Planète relâchée : {planetView.planet}");
    }
}

