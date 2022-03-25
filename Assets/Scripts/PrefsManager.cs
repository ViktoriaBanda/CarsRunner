using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    private const string CAR_KEY = "Car";
    
    public static void SaveLastSelectedCar(string name)
    {
        PlayerPrefs.SetString(CAR_KEY, name);
        PlayerPrefs.Save();
    }
    
    public static string GetLastSelectedCar()
    {
        return PlayerPrefs.GetString(CAR_KEY);
    }
}
