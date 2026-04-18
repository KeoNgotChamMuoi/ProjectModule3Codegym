public abstract class EnemyState : State
{
    protected EnemyController enemyController;
    protected EnemyStateMachine enemySM;
    public EnemyState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        this.enemySM = enemyStateMachine;
        this.enemyController = enemyStateMachine.enemyController; // Lưu tham chiếu đến EnemyController để dễ dàng truy cập trong các trạng thái con
    }
}
