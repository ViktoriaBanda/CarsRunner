using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectionButton : MonoBehaviour
{
    [SerializeField]
    private Image _carIcon;

    [SerializeField]
    private TextMeshProUGUI _carName;

    private CarSettings _car;

    public void Initialize(CarSettings car)
    {
        _car = car;
        _carIcon.sprite = car.Icon;
        _carName.text = car.Name;
    }

    [UsedImplicitly]
    public void OnClick()
    {
        // По клику на кнопку:
        // 1. Сохраняем в PlayerPrefs (хранилище, которое будет доступно даже после перезапуска игры)
        // название нашей выделенной машины
        PrefsManager.SaveLastSelectedCar(_car.Name);
        
        // 2. Сообщаем всем подписчикам, что мы выделили новую машину
        EventStreams.Game.Publish(new CarChangeEvent(_car));
    }
}