using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    private CompositeDisposable _subscriptions;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(GameConstants.LOBBY_SCENE);
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<StartGameEvent>(StartGameHandler)
        };
    }
    
    public void OnExit()
    {
        _subscriptions?.Dispose();
    }

    private void StartGameHandler(StartGameEvent eventData)
    {
        _stateMachine.Enter<GameState>();
    }
}
