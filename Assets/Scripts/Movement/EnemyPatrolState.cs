using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private float patrolTimer;
    private float patrolDuration = 5f; // Thời gian di chuyển trong trạng thái Patrol
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    Vector3 patrolDirection;
    public override void Enter()
    {
        base.Enter();
        // Chạy animation Patrol (Đặt tên TBool trong Animator là "isMoving")
        enemySM.enemyController.stats.animator.SetBool("isMoving", true);
        patrolTimer = patrolDuration; // Reset timer khi vào trạng thái Patrol
        patrolDirection = enemyController.transform.forward; // Di chuyển theo hướng hiện tại của enemy
    }
    public override void Update()
    {
        base.Update();
        // Di chuyển về phía trước theo hướng hiện tại của enemy
        enemyController.Move(patrolDirection);

        // Nếu thấy player, chuyển sang trạng thái Chase   
        if (enemySM.enemyController.CanSeePlayer())
        {
            enemySM.ChangeState(enemySM.chaseState);
            return;
        }
        // Chuyển sang trạng thái Idle sau khi hết thời gian Patrol  
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            patrolDirection = Quaternion.Euler(0, Random.Range(120f, 180f), 0) * patrolDirection; // Quay hướng di chuyển 90 độ
            enemySM.ChangeState(enemySM.idleState);
        }
        Debug.Log("Enemy is patrolling...");
    }
    public override void Exit()
    {
        enemySM.enemyController.stats.animator.SetBool("isMoving", false);
    }
}
