using System.Linq;
using Pool;
using UnityEngine;

public class CarSelectionButtons : MonoBehaviour
{
    [SerializeField]
    private CarSelectionButton _button;

    [SerializeField]
    private RectTransform _buttonsRoot;

    private MonoBehaviourPool<CarSelectionButton> _selectionButtonsPool;

    private void Awake()
    {
        _selectionButtonsPool = new MonoBehaviourPool<CarSelectionButton>(_button, _buttonsRoot);
    }

    public void Initialize(CarSettingsProvider settingsProvider)
    {
        var cars = settingsProvider.GetAllCars();
        foreach (var car in cars)
        {
            var carSelectionButton = _selectionButtonsPool.Take();
            carSelectionButton.Initialize(car);
        }

        // На старте игры, при инициализации, чтобы у нас всегда была выделенна хоть какая-то машина
        // мы вызываем этот метод, где кидаем сразу ивент о том, какая машина была выделена
        SelectLastSelectedCar(settingsProvider);
    }

    private void SelectLastSelectedCar(CarSettingsProvider settingsProvider)
    {
        var hasSelectedCar = string.IsNullOrEmpty(PrefsManager.GetLastSelectedCar());
        var lastSelectedCar = hasSelectedCar ?
            settingsProvider.GetAllCars().First() :
            settingsProvider.GetCar(PrefsManager.GetLastSelectedCar());
        
        EventStreams.Game.Publish(new CarChangeEvent(lastSelectedCar));
    }
}
