using UnityEngine;

public class CarSelectionView : MonoBehaviour
{
    [SerializeField]
    private CarSettingsProvider _carSettingsProvider;

    [SerializeField]
    private CarSelectionButtons _carSelectionButtons;

    private void Start()
    {
        _carSelectionButtons.Initialize(_carSettingsProvider);
    }
}
