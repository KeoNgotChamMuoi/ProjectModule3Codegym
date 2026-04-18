using UnityEngine;
public abstract class StateMachine : MonoBehaviour
{
    public static StateMachine Instance { get; private set; }
    public State CurrentState { get; set; }

    // Hàm khởi tạo trạng thái ban đầu
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = newState;
        if (CurrentState != null)
            CurrentState.Enter();
    }
    protected virtual void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.HandleInput();
            CurrentState.Update();
        }
    }
    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}
