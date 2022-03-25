using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collider)
    {
        EventStreams.Game.Publish(new CarCrashEvent(collider.gameObject));
    }
}
