using System;
using UnityEngine;

public class PlanetSystemController
{
    TimeModel timeModel;
    IPlanetEphemerisService ephemeris;

    PlanetView[] planets;

    // Number of sample points per orbit
    const int orbitSamples = 360;

    // Approximate orbital periods in days for each planet
    static readonly float[] orbitalPeriods = new float[]
    {
        88f,    // Mercury
        225f,   // Venus
        365f,   // Earth
        687f,   // Mars
        4333f,  // Jupiter
        10759f, // Saturn
        30687f, // Uranus
        60190f  // Neptune
    };

    public PlanetSystemController(
        TimeModel timeModel,
        IPlanetEphemerisService ephemeris,
        PlanetView[] planets)
    {
        this.timeModel = timeModel;
        this.ephemeris = ephemeris;
        this.planets = planets;

        timeModel.OnTimeChanged += UpdatePlanets;
    }

    void UpdatePlanets(DateTime time)
    {
        foreach (var planet in planets)
        {
            Vector3 pos = ephemeris.GetPlanetPosition(planet.planet, time);
            planet.SetPosition(pos);

            // Compute orbit trajectory (only when time changes, not every frame)
            if (planet.orbitLine != null)
            {
                ComputeOrbit(planet, time);
            }
        }
    }

    void ComputeOrbit(PlanetView planet, DateTime startTime)
    {
        float period = orbitalPeriods[(int)planet.planet];
        float stepDays = period / orbitSamples;

        Vector3[] points = new Vector3[orbitSamples];
        for (int i = 0; i < orbitSamples; i++)
        {
            DateTime t = startTime.AddDays(i * stepDays);
            points[i] = ephemeris.GetPlanetPosition(planet.planet, t);
        }

        planet.orbitLine.SetOrbitPoints(points);
    }
}
