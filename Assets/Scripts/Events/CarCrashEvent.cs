using SimpleEventBus.Events;
using UnityEngine;

public class CarCrashEvent : EventBase
{
    public GameObject CrashedCar { get; }
    public CarCrashEvent(GameObject crashedCar)
    {
        CrashedCar = crashedCar;
    }
}