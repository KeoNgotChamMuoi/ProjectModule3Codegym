using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Khi vào trạng thái chết, dừng mọi hoạt động của enemy
        enemyController.Stop();

        // Chuyển Rigidbody sang kinematic để tránh bị ảnh hưởng bởi vật lý sau khi chết                          
        enemyController.stats.rb.isKinematic = true;

        // Vô hiệu hóa collider để tránh va chạm sau khi chết
        Collider col = enemyController.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Kích hoạt Animation chết (Đặt tên Trigger trong Animator là "Die")
        enemyController.stats.animator.SetTrigger("Die");

        // Hủy đối tượng sau 10 giây để cho phép animation chết hoàn tất
        Object.Destroy(enemyController.gameObject, 10f);
        Debug.Log("Enemy is dead!");
    }

}
