using SimpleEventBus.Events;
using UnityEngine;

public class PointCollectedEvent : EventBase
{
    public GameObject GameCoin { get; }
    public int CoinValue { get; }
    public PointCollectedEvent(GameObject gameCoin, int coinValue)
    {
        GameCoin = gameCoin;
        CoinValue = coinValue;
    }
}