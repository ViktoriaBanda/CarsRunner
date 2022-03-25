using SimpleEventBus.Events;
using UnityEngine;

public class CarChangeEvent : EventBase
{
    public CarSettings SelectedCar { get; }

    public CarChangeEvent(CarSettings selectedCar)
    {
        SelectedCar = selectedCar;
    }
}