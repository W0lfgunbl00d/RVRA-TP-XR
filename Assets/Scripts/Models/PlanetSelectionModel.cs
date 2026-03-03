using System;
using UnityEngine;

public class PlanetSelectionModel
{
    public PlanetView SelectedPlanet { get; private set; }
    
    public event Action<PlanetView> OnSelectionChanged;

    public void Select(PlanetView planet)
    {
        if (SelectedPlanet == planet) return;

        SelectedPlanet = planet;
        Debug.Log($"[SELECTION] Planète sélectionnée : {(planet != null ? planet.planet.ToString() : "aucune")}");
        OnSelectionChanged?.Invoke(SelectedPlanet);
    }

    public void Deselect()
    {
        Select(null);
    }
}

