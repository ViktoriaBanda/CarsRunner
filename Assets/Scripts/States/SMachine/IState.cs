public interface IState 
{
    void Initialize(StateMachine stateMachine);
    void OnEnter();
    void OnExit();
}