using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State currentState;
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        if (currentState != null)
            currentState.Enter();
    }
    void Update()
    {
        if (currentState != null)
            currentState.Update();
    }
}
