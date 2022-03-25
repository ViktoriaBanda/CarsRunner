using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarSettingsProvider _carSettingsProvider;
    private StateMachine _stateMachine;
    
    private void Awake()
    {
        _carSettingsProvider.Initialize();
        DontDestroyOnLoad(gameObject);
        CreateAndInitializeStateMachine();
    }
    
    private void CreateAndInitializeStateMachine()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<LobbyState>(),
            GetComponent<GameState>(),
            GetComponent<GameOverState>()
        );
    
        _stateMachine.Initialize();
        _stateMachine.Enter<LobbyState>();
    }
}
