using System;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField]
    private RoadsPool _objectPool;
    
    private Trigger _trigger;
    private GameObject _currentPiece;
    private bool _isFirstPiece = true;
    private static readonly Vector3 ROADS_START_POSITION = new Vector3(-12f, 0, -10f);

    private IDisposable _subscription;
    private void Start()
    {
        _subscription = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<TriggerEnterEvent>(TriggerEnterEventHandler),
            EventStreams.Game.Subscribe<PressStartButtonEvent>(PressStartButtonEventHandler)
        };
        
        ShowNewPieceInCorrectPosition();
    }

    private void ShowNewPieceInCorrectPosition()
    {
        var roadToSpawn = _objectPool.TakeFromPool();
        
        if (!_isFirstPiece)
        {
            CalculatePosition(roadToSpawn); 
            EventStreams.Game.Publish(new SpawnMapEvent(roadToSpawn));
        }
        _isFirstPiece = false;

        _currentPiece = roadToSpawn;

        if(_objectPool.NotUsedItems.Count == 0)
        {
            var itemToRelease = _objectPool.UsedItems[0];
            _objectPool.Release(itemToRelease, ROADS_START_POSITION);
        }
    }

    private void CalculatePosition(GameObject roadToSpawn)
    {
        var endOfCurrentRoad = _currentPiece.GetComponent<RoadPiece>().End.transform.position;
        roadToSpawn.transform.position = endOfCurrentRoad;
    }

    private void TriggerEnterEventHandler(TriggerEnterEvent eventData)
    {
        ShowNewPieceInCorrectPosition();
    }
    
    private void PressStartButtonEventHandler(PressStartButtonEvent eventData)
    {
        _objectPool.ReleaseAll(ROADS_START_POSITION);
        _isFirstPiece = true;
        ShowNewPieceInCorrectPosition();
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
