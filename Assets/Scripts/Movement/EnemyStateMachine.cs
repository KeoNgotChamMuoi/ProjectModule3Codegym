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
    public void Instantiate(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

}

