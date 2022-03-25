using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarCharacteristicView : MonoBehaviour
{
    [SerializeField]
    private Scrollbar _value;
    [SerializeField]
    private TextMeshProUGUI _name;

    public void Initialize(CarCharacteristic characteristic)
    {
        // меняем % заполнения
        _value.size = Mathf.Clamp01(characteristic.Value / (float)characteristic.MaxValue);
        // устанавливаем название характеристики
        _name.text = characteristic.Name;
    }
}
