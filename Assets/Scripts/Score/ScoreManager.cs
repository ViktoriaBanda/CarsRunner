using System;
using SimpleEventBus.Disposables;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<float, float> PointCollected;
    
    [SerializeField] private float _calculatingTime = 2f;
    private float _score;
    private IDisposable _subscription;

    private void Awake()
    {
        _subscription = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PointCollectedEvent>(PointCollectedEventHandler),
            EventStreams.Game.Subscribe<CarCrashEvent>(CarCrashEventHandler)
        };
    }

    private void PointCollectedEventHandler(PointCollectedEvent eventData)
    {
        _score += eventData.CoinValue;
        PointCollected?.Invoke(_score, _calculatingTime);
    }
    
    private void CarCrashEventHandler(CarCrashEvent eventData)
    {
        _score = 0;
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
