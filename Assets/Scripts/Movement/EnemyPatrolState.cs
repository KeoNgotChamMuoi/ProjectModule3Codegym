using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private float patrolTimer;
    private float patrolDuration = 5f; // Thời gian di chuyển trong trạng thái Patrol
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        // Chạy animation Patrol (Đặt tên Trigger/Bool trong Animator là "isPatrolling")
        enemySM.enemyController.stats.animator.SetBool("isMoving", true);
        patrolTimer = patrolDuration; // Reset timer khi vào trạng thái Patrol
    }
    public override void Update()
    {
        base.Update();
        // Di chuyển về phía trước theo hướng hiện tại của enemy
        enemyController.Move(enemyController.transform.forward);

        // Nếu thấy player, chuyển sang trạng thái Chase   
        if (enemySM.enemyController.CanSeePlayer())
        {
            enemySM.ChangeState(enemySM.chaseState);
            return; // Thoát khỏi hàm Update để tránh tiếp tục kiểm tra timer
        }
        // Chuyển sang trạng thái Idle sau khi hết thời gian Patrol  
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0f)
        {
            enemyController.transform.Rotate(0f, 100f, 0f); // Quay ngược lại sau khi hết thời gian Patrol
            enemySM.ChangeState(enemySM.idleState);
        }

    }
    public override void Exit()
    {
        enemySM.enemyController.stats.animator.SetBool("isMoving", false);
    }
}
