using SimpleEventBus.Events;
using UnityEngine;

public class SpawnMapEvent: EventBase
{
    public GameObject RoadToSpawn { get; }
    public SpawnMapEvent(GameObject roadToSpawn)
    {
        RoadToSpawn = roadToSpawn;
    }
}