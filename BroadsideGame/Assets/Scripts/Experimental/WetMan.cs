using NUnit.Framework;
using UnityEngine;

public class WetMan : WeMan
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {

        declareWeather();

        triggerWeatherEvent("tada");

      
    }
    void eventTriggered()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
