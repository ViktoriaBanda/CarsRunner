using System;
using System.Collections;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    private IDisposable _subscription;
    private void Start()
    {
        _subscription = EventStreams.Game.Subscribe<PointCollectedEvent>(PointCollectedEventHandel);
    }

    private void PointCollectedEventHandel(PointCollectedEvent eventData)
    {
        StartCoroutine(HideAndShowCoin(eventData.GameCoin));
    }

    private IEnumerator HideAndShowCoin(GameObject coin)
    {
        coin.SetActive(false);
        yield return new WaitForSeconds(4f);
        coin.SetActive(true);
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
