using UnityEngine;
using System;

public class AppBootstrapper : MonoBehaviour
{
    public SolarSystemConfig config;

    public PlanetView[] planets;

    TimeModel timeModel;
    PlanetSystemController controller;
	TimeController timeController;

    void Start()
    {
        Debug.Log("[BOOT] Initializing application");

        timeModel = new TimeModel();

        var ephemeris = new PlanetEphemerisService();

        controller = new PlanetSystemController(
            timeModel,
            ephemeris,
            planets
        );

        // Configure orbit visibility from config
        foreach (var planet in planets)
        {
            if (planet.orbitLine != null)
            {
                planet.orbitLine.SetVisible(config.showOrbits);
            }
        }

        timeModel.SetTime(DateTime.Now);

		timeController = gameObject.AddComponent<TimeController>();
		timeController.Init(timeModel);
    }
}
