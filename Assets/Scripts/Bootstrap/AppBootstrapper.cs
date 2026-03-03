using System;
using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
    [Header("Config (assignée via l'inspecteur)")]
    [SerializeField] SolarSystemConfig config;

    [Header("Views (assignées via l'inspecteur)")]
    [SerializeField] PlanetView[] planets;

    [Header("Solar System (assigné via l'inspecteur)")]
    [Tooltip("Le transform racine du système solaire (pour le scale)")]
    [SerializeField] Transform solarSystemRoot;

    [Header("Debug (assigné via l'inspecteur)")]
    [Tooltip("Panneau de debug visible dans le casque")]
    [SerializeField] DebugOverlay debugOverlay;

    TimeModel timeModel;
    PlanetSystemController controller;
    ScaleController scaleController;
    TimeController timeController;

    void Start()
    {
        Debug.Log("[BOOT] Initializing application...");

        // --- Validation des références injectées ---
        if (config == null)
        {
            Debug.LogError("[BOOT] SolarSystemConfig manquant ! Assignez-le dans l'inspecteur.");
            return;
        }
        if (planets == null || planets.Length == 0)
        {
            Debug.LogError("[BOOT] Aucune PlanetView assignée ! Assignez-les dans l'inspecteur.");
            return;
        }

        // --- Modèle ---
        timeModel = new TimeModel();
        Debug.Log("[BOOT] TimeModel créé");

        // --- Services ---
        var ephemeris = new PlanetEphemerisService();
        Debug.Log("[BOOT] PlanetEphemerisService créé");

        // --- Controller planètes ---
        controller = new PlanetSystemController(
            timeModel,
            ephemeris,
            planets
        );
        Debug.Log($"[BOOT] PlanetSystemController créé ({planets.Length} planètes)");

        // --- Orbites ---
        int orbitCount = 0;
        foreach (var planet in planets)
        {
            if (planet.orbitLine != null)
            {
                planet.orbitLine.SetVisible(config.showOrbits);
                orbitCount++;
            }
        }
        Debug.Log($"[BOOT] Orbites configurées : {orbitCount}/{planets.Length} (visible={config.showOrbits})");

        // --- Scale ---
        if (solarSystemRoot != null)
        {
            scaleController = new ScaleController(
                solarSystemRoot,
                config.initialScale,
                config.minScale,
                config.maxScale
            );

            // ScaleInteraction utilise Update() pour lire le thumbstick (justifié)
            var scaleInteraction = gameObject.AddComponent<ScaleInteraction>();
            scaleInteraction.Init(scaleController);
            Debug.Log("[BOOT] ScaleController + ScaleInteraction créés");
        }
        else
        {
            Debug.LogWarning("[BOOT] SolarSystemRoot non assigné — contrôle d'échelle désactivé");
        }

        // --- Debug Overlay ---
        if (debugOverlay != null)
        {
            debugOverlay.Init(timeModel);
            debugOverlay.SetVisible(config.showDebugOverlay);
            Debug.Log($"[BOOT] DebugOverlay initialisé (visible={config.showDebugOverlay})");
        }
        else
        {
            Debug.LogWarning("[BOOT] DebugOverlay non assigné — pas de panneau de debug");
        }

        // --- Temps ---
        timeModel.SetTime(DateTime.Now);

        // TimeController utilise Update() car il doit faire avancer le temps chaque frame
        timeController = gameObject.AddComponent<TimeController>();
        timeController.Init(timeModel);
        Debug.Log("[BOOT] TimeController initialisé");

        Debug.Log("[BOOT] Application prête ✓");
    }
}
