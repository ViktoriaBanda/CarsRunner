using System;
using UnityEngine;

[Serializable]
public class CarSettings
{
    public string PlayerName;
    public string Name;
    public GameObject Prefab;
    public Sprite Icon;
    public CarCharacteristic[] Characteristics;
}
