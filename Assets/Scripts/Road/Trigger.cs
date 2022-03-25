using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventStreams.Game.Publish(new TriggerEnterEvent());
    }
}
