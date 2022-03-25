using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotationHandler : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        EventStreams.Game.Publish(new RotatingCarEvent(eventData.delta.x));
    }
}