using UnityEngine;

namespace Game.Core
{

    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(EnemyStateMachine))]
    public class EnemyBase : BaseEntity
    {
        [Header("References")]

        [Header("Enemy Components")]
        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody rb;


        [Header("Combat Stats")]
        public int damageOutput = 10;
        public float attackRange = 1.5f;
        public float attackCooldown = 1f;
        public int maxHealth = 100;
        protected int currentHealth;

        [Header("Movement Stats")]
        public float MoveSpeed = 2f;   // Tốc độ di chuyển khi đang patrol
        public float ChaseSpeed = 3f; // Tốc độ khi đang chase player
        public float rotationSpeed = 5f; // Tốc độ xoay khi theo dõi player


        [Header("Detection")]
        public Transform dectectionPoint; // Điểm xuất phát của raycast để phát hiện player
        public float detectionRange = 5f; // Khoảng cách phát hiện player 
        public float viewAngle = 60f; // Góc nhìn của enemy để phát hiện player 
        public LayerMask detectionMask;   // LayerMask để xác định những gì enemy có thể phát hiện (ví dụ: player, tường)


        [Header("Patrol Settings")]
        public Transform[] patrolPoints;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;

            // Khóa xoay vật lý để tránh quái bị ngã
            if (rb != null)
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        protected virtual void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var player = col.gameObject.GetComponent<BaseEntity>();
                if (player != null)
                {
                    player.TakeDamage(damageOutput);
                }
                Debug.Log($"{gameObject.name} attacked {col.gameObject.name} for {damageOutput} damage.");
            }

        }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
        }

        public override void Die()
        {
            base.Die();

        }

    }
}