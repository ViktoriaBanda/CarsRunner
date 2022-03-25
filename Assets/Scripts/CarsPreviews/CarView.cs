using System.Collections.Generic;
using Pool;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField]
    private Transform _carRoot;

    [SerializeField] 
    private CarSettingsProvider _carSettingsProvider;

    private CompositeDisposable _subscriptions;
    private GameObject _car;
    
    private Dictionary<string, GameObject> _carInstances = new();
    
    private void Awake()
    {
        var allCars = _carSettingsProvider.GetAllCars();
        foreach (var carSettings in allCars)
        {
            _carInstances[carSettings.Name] = Instantiate(carSettings.Prefab, _carRoot);
            _carInstances[carSettings.Name].SetActive(false);
        }
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CarChangeEvent>(CarChangeEventHandler),
            EventStreams.Game.Subscribe<RotatingCarEvent>(RotatingCarEventHandler)
        };
    }
    private void CarChangeEventHandler(CarChangeEvent eventData)
    {
        if (_car != null)
        {
            _car.SetActive(false);
        }

        _car = _carInstances[eventData.SelectedCar.Name];
        _car.transform.rotation = Quaternion.identity;
        _car.SetActive(true);
    }
    
    private void RotatingCarEventHandler(RotatingCarEvent eventData)
    {
        if (_car != null)
        {
            _car.transform.Rotate(0, -eventData.RotationY, 0);
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
