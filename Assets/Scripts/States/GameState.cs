using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, IState 
{
    [SerializeField]
    private CarSettingsProvider _carSettingsProvider;
    
    private StateMachine _stateMachine;
    private PlayerController _playerController;
    private CameraController _cameraController;
    
    private IDisposable _subscription;
    private GameObject _selectedCarPrefab;
    private bool _isFirstEnter = true;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _subscription = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CarCrashEvent>(CarCrashEventHandler),
            EventStreams.Game.Subscribe<GameSceneLoadedEvent>(GameSceneLoadedEventHandler)
        };

        if (_isFirstEnter)
        {
            var lastSelectedCarName = PrefsManager.GetLastSelectedCar();
            var lastSelectedCar = _carSettingsProvider.GetCar(lastSelectedCarName);
            _selectedCarPrefab = lastSelectedCar.Prefab;
            
            SceneManager.LoadScene(GameConstants.GAME_SCENE);
            _isFirstEnter = false;
            return;
        }
        
        InitializeGameAndCameraControllers();
        _playerController.enabled = true;
    }

    public void OnExit()
    {
        _subscription.Dispose();    
    }

    private void GameSceneLoadedEventHandler(GameSceneLoadedEvent eventData)
    {
        _playerController = FindObjectOfType<PlayerController>(); 
        _cameraController = FindObjectOfType<CameraController>();
        
        InitializeGameAndCameraControllers();
    }

    private void InitializeGameAndCameraControllers()
    {
        _playerController.Initialize(_selectedCarPrefab);
        _cameraController.Initialize(_playerController);
    }

    private void CarCrashEventHandler(CarCrashEvent eventData)
    {
        _playerController.enabled = false;
        _stateMachine.Enter<GameOverState>();
    }
}
