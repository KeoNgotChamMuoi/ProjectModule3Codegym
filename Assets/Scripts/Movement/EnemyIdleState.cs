using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float idleTimer;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        this.enemySM = enemyStateMachine;
    }
    public override void Enter()
    {
        // Gọi phương thức Enter của lớp cơ sở để đảm bảo các thiết lập chung được thực hiện
        base.Enter();
        // Chạy animation Idle (Đặt tên Trigger/Bool trong Animator là "isIdle")
        enemySM.enemyController.stats.animator.SetBool("isIdle", true);
        idleTimer = 2f;    // Thời gian chờ trước khi chuyển sang trạng thái tiếp theo
    }

    public override void Update()
    {
        base.Update();
        idleTimer -= Time.deltaTime;

        // Nếu thấy player, chuyển sang trạng thái Chase   
        if (enemySM.enemyController.CanSeePlayer())
        {
            enemySM.ChangeState(enemySM.chaseState);
            return; // Thoát khỏi hàm Update để tránh tiếp tục kiểm tra timer
        }

        // Chuyển sang trạng thái Patrol sau khi hết thời gian idle
        if (idleTimer <= 0f)
        {
            enemySM.ChangeState(enemySM.patrolState);
        }
    }
    public override void Exit()
    {
        enemySM.enemyController.stats.animator.SetBool("isIdle", false);
    }
}
