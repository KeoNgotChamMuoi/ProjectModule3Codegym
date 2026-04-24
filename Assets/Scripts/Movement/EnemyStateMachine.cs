
public class EnemyStateMachine : StateMachine
{
    public EnemyIdleState idleState;
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;

    public EnemyController enemyController;

    public EnemyStateMachine(EnemyController enemyController)
    {
        this.enemyController = enemyController;
        idleState = new EnemyIdleState(this);
        patrolState = new EnemyPatrolState(this);
        chaseState = new EnemyChaseState(this);
        attackState = new EnemyAttackState(this);
        deadState = new EnemyDeadState(this);
    }

    private void Awake()
    {
        if (enemyController == null)
        {
            enemyController = GetComponent<EnemyController>();
            //Debug.LogError("EnemyController reference is missing in EnemyStateMachine.");
        }
    }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

}

