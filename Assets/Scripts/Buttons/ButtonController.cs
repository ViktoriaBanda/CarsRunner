using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _gameOverButton;
    private IDisposable _subscription;

    private void Start()
    {
        HideButtons();
        _subscription = EventStreams.Game.Subscribe<CarCrashEvent>(CarCrashEventHandler);
    }
    
    public void PressStartButton()
    {
        EventStreams.Game.Publish(new PressStartButtonEvent());
        HideButtons();
    }

    private void CarCrashEventHandler(CarCrashEvent obj)
    {
        ShowButtons();
    }

    private void ShowButtons()
    {
        _startButton.gameObject.SetActive(true);
        _gameOverButton.gameObject.SetActive(true);
    }

    private void HideButtons()
    {
        _startButton.gameObject.SetActive(false);
        _gameOverButton.gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
