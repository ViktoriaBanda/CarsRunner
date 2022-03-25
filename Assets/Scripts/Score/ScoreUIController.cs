using System;
using System.Collections;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _pointsScoredLabel;
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    
    private float _currentScore;
    private float _totalScore;
    private IDisposable _subscription;
    
    private void Awake()
    {
        _scoreManager.PointCollected += SetScore;
        _subscription = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CarCrashEvent>(CarCrashEventHandler),
            EventStreams.Game.Subscribe<PressStartButtonEvent>(PressStartButtonEventHandler)
        };
    }

    private void SetScore(float newScore, float calculatingTime)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMoneyCalculation(newScore, calculatingTime));
    }


    private IEnumerator ShowMoneyCalculation(float newScore, float calculatingTime)
    {
        _totalScore = newScore;
        var currentTime = 0f;
        float incrementScorePerFrame;

        while (currentTime < calculatingTime)
        {
            var interpolatingScore = Mathf.Lerp(_currentScore, newScore, currentTime / calculatingTime);
            incrementScorePerFrame = Mathf.CeilToInt(interpolatingScore);
            currentTime += Time.deltaTime;
            AddScoreToScoreLabel(incrementScorePerFrame);
            
            yield return null;
        }

        _currentScore = newScore;
    }

    private void AddScoreToScoreLabel(float incrementScorePerFrame)
    {
        _pointsScoredLabel.text = incrementScorePerFrame.ToString();
    }
    
    private void CarCrashEventHandler(CarCrashEvent eventData)
    {
        _scoreLabel.text = "Счёт: " + _totalScore;
        _scoreLabel.gameObject.SetActive(true);
    }

    private void PressStartButtonEventHandler(PressStartButtonEvent eventData)
    {
        StopAllCoroutines();
        _currentScore = 0;
        _pointsScoredLabel.text = "0";
        _scoreLabel.gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}