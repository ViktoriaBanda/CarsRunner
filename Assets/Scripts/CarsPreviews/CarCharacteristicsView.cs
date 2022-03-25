using Pool;
using SimpleEventBus.Disposables;
using UnityEngine;

public class CarCharacteristicsView : MonoBehaviour
{
    [SerializeField]
    private RectTransform _characteristicsRoot;
    
    [SerializeField]
    private CarCharacteristicView _characteristicView;
    
    private CompositeDisposable _subscriptions;
    
    private MonoBehaviourPool<CarCharacteristicView> _characteristicsPool;
    
    private void Awake()
    {
        _characteristicsPool = new MonoBehaviourPool<CarCharacteristicView>(_characteristicView, 
            _characteristicsRoot);
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CarChangeEvent>(OnCarSelected)
        };
    }

    private void OnCarSelected(CarChangeEvent eventData)
    {
        // Все характеристики возвращаем в пул и деактивируем.
        _characteristicsPool.ReleaseAll();
        
        // Достаем из данных ивента выделенную машину
        var selectedCar = eventData.SelectedCar;
        // Проходимся по всем характеристикам машины
        foreach (var characteristic in selectedCar.Characteristics)
        {
            _characteristicsPool.Take().Initialize(characteristic);
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
