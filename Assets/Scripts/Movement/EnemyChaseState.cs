using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        // Chạy animation Chase (Đặt tên Trigger/Bool trong Animator là "isChasing")
        enemySM.enemyController.stats.animator.SetBool("isChasing", true);
    }

    public override void Update()
    {
        base.Update();

        if (!enemySM.enemyController.CanSeePlayer())
        {
            enemySM.ChangeState(enemySM.patrolState);
            return;
        }

        if (enemyController.Target != null)
        {
            Vector3 direction = enemyController.Target.position - enemyController.transform.position;
            enemyController.Move(direction, isChasing: true);
        }
    }

    public override void Exit()
    {
        enemySM.enemyController.stats.animator.SetBool("isChasing", false);
    }
}
