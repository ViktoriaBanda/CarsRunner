using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSettingsProvider", menuName = "CarSettings")]
public class CarSettingsProvider : ScriptableObject
{
    [SerializeField]
    private CarSettings[] _carSettings;
    
    private readonly Dictionary<string, CarSettings> _carSettingByName = new Dictionary<string, CarSettings>();
    
    public void Initialize()
    {
        foreach (var carSettings in _carSettings)
        {
            _carSettingByName[carSettings.Name] = carSettings;
        }
    }
    
        public CarSettings[] GetAllCars()
        {
            return _carSettings;
        }
    
        public CarSettings GetCar(string carName)
        {
            return _carSettingByName[carName];
        }
}
