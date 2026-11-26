using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeMan : MonoBehaviour
{

    protected List<string> weatherEventName;
   public abstract void eventTriggered();

    protected virtual void declareWeather()
    {
        weatherEventName = new List<string>();
    }

    protected abstract void triggerWeatherEvent(string weatherEventName);



}
