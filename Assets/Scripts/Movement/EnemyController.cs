using Game.Core;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public EnemyBase stats; // Tham chiếu đến file EnemyBase
    [HideInInspector] public EnemyStateMachine enemyStateMachine;


    public Transform Target;
    private void Awake()
    {
        if (stats == null) stats = GetComponent<EnemyBase>();
        // Khởi tạo EnemyStateMachine và truyền tham chiếu đến EnemyController
        enemyStateMachine = new EnemyStateMachine(this);

        // Lấy component Animator và Rigidbody
        stats.animator = GetComponent<Animator>();
        stats.rb = GetComponent<Rigidbody>();

        stats.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Khởi tạo state machine với trạng thái ban đầu là idle   
        enemyStateMachine.Initialize(enemyStateMachine.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        // Gọi Update của state machine để xử lý logic của trạng thái hiện tại 
        enemyStateMachine.CurrentState?.Update();
    }
    void FixedUpdate()
    {
        // Gọi FixedUpdate của state machine để xử lý logic vật lý của trạng thái hiện tại
        enemyStateMachine.CurrentState.FixedUpdate();
    }

    public void Move(Vector3 direction, bool isChasing = false)
    {
        float speed = isChasing ? stats.ChaseSpeed : stats.MoveSpeed;

        // Tính toán vector di chuyển dựa trên hướng và tốc độ, sau đó di chuyển Rigidbody
        Vector3 moveDir = direction.normalized;
        // Tính toán vận tốc dựa trên hướng (giữ nguyên vận tốc Y của trọng lực)
        Vector3 velocity = moveDir * speed;
        stats.rb.velocity = new Vector3(moveDir.x, stats.rb.velocity.y, moveDir.z);

        Debug.Log($"Enemy is moving with velocity: {velocity}");
        // Xoay nhân vật mượt mà về hướng di chuyển
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, stats.rotationSpeed * Time.deltaTime);
        }
    }

    public void Stop()
    {
        if (stats.rb != null)
            stats.rb.velocity = new Vector3(0, stats.rb.velocity.y, 0);
    }
    public bool CanSeePlayer()
    {
        // Sử dụng OverlapSphere để tìm tất cả các collider trong phạm vi phát hiện
        Collider[] targets = Physics.OverlapSphere(transform.position, stats.detectionRange, stats.detectionMask);

        foreach (var target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Vector3 dirToPlayer = (target.transform.position - stats.dectectionPoint.position).normalized;

                // Kiểm tra góc nhìn FOV
                if (Vector3.Angle(transform.forward, dirToPlayer) < stats.viewAngle / 2f)
                {
                    float distToPlayer = Vector3.Distance(stats.dectectionPoint.position, target.transform.position);

                    // Raycast kiểm tra vật cản
                    if (!Physics.Raycast(stats.dectectionPoint.position, dirToPlayer, distToPlayer, LayerMask.GetMask("Obstacle")))
                    {
                        targets = target.GetComponentsInParent<Collider>(); // Lấy tất cả collider của player (bao gồm cả collider con)
                        return true;
                    }

                }
            }
        }
        return false;
    }


    //private void OnDrawGizmos()
    //{
    //    if (stats.dectectionPoint == null) return;

    //    // Vẽ tầm nhìn hình nón trong Scene để dễ debug
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(transform.position, stats.detectionRange);

    //    Vector3 leftBoundary = Quaternion.AngleAxis(-stats.viewAngle / 2, Vector3.up) * transform.forward;
    //    Vector3 rightBoundary = Quaternion.AngleAxis(stats.viewAngle / 2, Vector3.up) * transform.forward;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(stats.dectectionPoint.position, leftBoundary * stats.detectionRange);
    //    Gizmos.DrawRay(stats.dectectionPoint.position, rightBoundary * stats.detectionRange);
    //}
}
