using System;
using UnityEngine;

public class GameOverState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    private IDisposable _subscription;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _subscription = EventStreams.Game.Subscribe<PressStartButtonEvent>(PressStartButtonEventHandler);
    }

    public void OnExit()
    {
        _subscription.Dispose();
    }
    
    private void PressStartButtonEventHandler(PressStartButtonEvent obj)
    {
        _stateMachine.Enter<GameState>();
    }
}
