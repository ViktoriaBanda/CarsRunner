using System;
using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _enemyPrefabs;

    private List<GameObject> _enemies = new();
    private IDisposable _subscription;

    private void Start()
    {
        _subscription = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CarCrashEvent>(CarCrashEventHandler),
            EventStreams.Game.Subscribe<PressStartButtonEvent>(PressStartButtonEventHandler),
            EventStreams.Game.Subscribe<SpawnMapEvent>(SpawnMapEventHandler)
        };
    }

    private void SpawnMapEventHandler(SpawnMapEvent eventData)
    {
        SpawnEnemies(eventData.RoadToSpawn.GetComponent<RoadPiece>());
    }

    private void SpawnEnemies(RoadPiece roadToSpawn)
    {
        for (var i = 0; i < _enemyPrefabs.Length; i++)
        {
            var instance = Instantiate(_enemyPrefabs[i]);
            _enemies.Add(instance);
            if (i == 0)
            {
                StartCoroutine(MoveEnemy(instance, roadToSpawn.StartSpawnFollowingCar.transform.position,
                roadToSpawn.EndSpawnFollowingCar.transform.position));
            }
            else
            {
                StartCoroutine(MoveEnemy(instance, roadToSpawn.StartSpawnMeetingCar.transform.position,
                    roadToSpawn.EndSpawnMeetingCar.transform.position));
            }
        }
    }

    private IEnumerator MoveEnemy(GameObject instance, Vector3 start, Vector3 end)
    {
        var currentTime = 0f;
        var time = 15f;
        while (currentTime < time)
        {
            instance.transform.position = Vector3.Lerp(start, end, currentTime / time);
            currentTime += Time.deltaTime;
            
            yield return null;
        }
        Destroy(instance);
        _enemies.Remove(instance);
    }
    
    private void CarCrashEventHandler(CarCrashEvent eventData)
    {
        StopAllCoroutines();
    }
    
    private void PressStartButtonEventHandler(PressStartButtonEvent eventData)
    {
        while (_enemies.Count > 0)
        {
            var enemyToDestroy = _enemies[0];
            _enemies.RemoveAt(0);
            Destroy(enemyToDestroy);
        }
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
