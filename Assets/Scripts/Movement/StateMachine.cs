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
        currentState = newState;
        if (currentState != null)
            currentState.Enter();
    }
    void Update()
    {
        if (currentState != null)
            currentState.Update();
    }
    void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedUpdate();
    }
}
