using Game.Core;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public EnemyBase stats; // Tham chiếu đến file EnemyBase
    public EnemyStateMachine enemyStateMachine;


    public Transform Target;
    private void Awake()
    {
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
        enemyStateMachine.Instantiate(enemyStateMachine.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        // Gọi Update của state machine để xử lý logic của trạng thái hiện tại 
        enemyStateMachine.CurrentState.Update();
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
        // Tính toán vận tốc dựa trên hướng (giữ nguyên vận tốc Y của trọng lực)
        Vector3 moveDir = direction.normalized * speed;
        stats.rb.velocity = new Vector3(moveDir.x, stats.rb.velocity.y, moveDir.z);

        // Xoay nhân vật mượt mà về hướng di chuyển
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, stats.rotationSpeed * Time.deltaTime);
        }
    }

    public void Stop()
    {
        stats.rb.velocity = new Vector3(0, stats.rb.velocity.y, 0);
    }
    public bool CanSeePlayer()
    {
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
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        if (stats.dectectionPoint == null) return;

        // Vẽ tầm nhìn hình nón trong Scene để dễ debug
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stats.detectionRange);

        Vector3 leftBoundary = Quaternion.AngleAxis(-stats.viewAngle / 2, Vector3.up) * transform.forward;
        Vector3 rightBoundary = Quaternion.AngleAxis(stats.viewAngle / 2, Vector3.up) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(stats.dectectionPoint.position, leftBoundary * stats.detectionRange);
        Gizmos.DrawRay(stats.dectectionPoint.position, rightBoundary * stats.detectionRange);
    }
}
