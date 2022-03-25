using SimpleEventBus.Events;
using UnityEngine;

public class RotatingCarEvent : EventBase
{
    public float RotationY { get; }

    public RotatingCarEvent(float rotationY)
    {
        RotationY = rotationY;
    }
}
