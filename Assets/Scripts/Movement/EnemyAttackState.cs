using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackTimer;
    private float attackCooldown = 1f;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyController.Stop();
        attackTimer = 0f;
        // Play attack animation here
    }

    public override void Update()
    {
        base.Update();
        if (enemyController.Target == null)
        {
            enemySM.ChangeState(enemySM.idleState);
            return;
        }
        float distanceToTarget = Vector3.Distance(enemyController.transform.position, enemyController.Target.position);

        //
        if (distanceToTarget > enemyController.stats.detectionRange)
        {
            enemySM.ChangeState(enemySM.chaseState);
            return;
        }
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;
        }

        Vector3 lookToTaget = enemyController.Target.position - enemyController.transform.position;
        enemyController.Move(lookToTaget * 0.01f);    // Di chuyển nhẹ về phía target để giữ khoảng cách khi tấn công

    }

    public void PerformAttack()
    {
        // Kích hoạt Animation Tấn công
        enemyController.stats.animator.SetTrigger("Attack");

        // Logic gây sát thương (Melee hoặc Ranged) sẽ nằm ở đây hoặc qua Animation Event
        Debug.Log("Enemy attacks!");
    }
}