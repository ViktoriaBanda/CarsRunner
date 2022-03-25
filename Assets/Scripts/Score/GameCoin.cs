using UnityEngine;

public class GameCoin : MonoBehaviour
{
     [SerializeField]
     private int _coinValue;
     
     private void OnTriggerEnter(Collider collider)
     {
         EventStreams.Game.Publish(new PointCollectedEvent(gameObject, _coinValue));
     }
}
