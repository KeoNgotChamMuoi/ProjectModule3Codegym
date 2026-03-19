using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player3D player;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerAttackState attackState;
    void Start()
    {
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);

        ChangeState(idleState);
    }
    void Update()
    {
        if (currentState != null)
            currentState.Update();
    }
    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedUpdate();
    }

}

public class PlayerIdleState : State
{
    private PlayerStateMachine playerState;
    public PlayerIdleState(PlayerStateMachine playerState) : base(playerState)
    {
        this.playerState = playerState;
    }
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }
    public override void Update()
    {
        var input = InputManager.Instance;

        // Move → chuyển state
        if (input.MoveInput != Vector2.zero)
        {
            playerState.ChangeState(playerState.moveState);
            return;
        }

        // Attack
        if (input.MouseLeftDown)
        {
            playerState.ChangeState(playerState.attackState);
            return;
        }
    }
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

public class PlayerMoveState : State
{
    private PlayerStateMachine playerState;
    Rigidbody rb;
    Vector3 currentVelocity;
    public PlayerMoveState(PlayerStateMachine playerState) : base(playerState)
    {
        this.playerState = playerState;
    }
    public override void Enter()
    {
        rb = playerState.GetComponent<Rigidbody>();
        Debug.Log("Entering Move State");
    }

    public override void Update()
    {
        var input = InputManager.Instance;
        if (input.MoveInput == Vector2.zero)
        {
            playerState.ChangeState(playerState.idleState);
            return;
        }
        if (input.MouseLeftDown)
        {
            playerState.ChangeState(playerState.attackState);
            return;
        }
        // Add movement logic here
    }

    public override void FixedUpdate()
    {
        Move();
    }
    public override void Exit()
    {
        Debug.Log("Exiting Move State");
    }

    public void Move()
    {
        Vector2 input = InputManager.Instance.MoveInput;
        //float speed = Player3D.Instance.moveSpeed;

        // Hướng theo camera (TPS)
        Vector3 camForward = PlayerController.Instance.cameraPivot.forward;
        Vector3 camRight = PlayerController.Instance.cameraPivot.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // ===== MOVE =====
        Vector3 moveDir = camForward * input.y + camRight * input.x;

        // KHÔNG XOAY PLAYER Ở ĐÂY

        Vector3 velocity = rb.velocity;

        velocity.x = moveDir.x * Player3D.Instance.moveSpeed;
        velocity.z = moveDir.z * Player3D.Instance.moveSpeed;

        rb.velocity = velocity;
    }
    //public void Move()
    //{
    //    Vector2 input = InputManager.Instance.MoveInput;

    //    Transform cam = PlayerController.Instance.cameraPivot;

    //    // ===== CAMERA DIRECTION =====
    //    Vector3 camForward = cam.forward;
    //    Vector3 camRight = cam.right;

    //    camForward.y = 0;
    //    camRight.y = 0;

    //    camForward.Normalize();
    //    camRight.Normalize();

    //    // ===== MOVE DIR =====
    //    Vector3 moveDir = camForward * input.y + camRight * input.x;

    //    // ===== SMOOTH VELOCITY=====
    //    Vector3 targetVelocity = moveDir * Player3D.Instance.moveSpeed;

    //    currentVelocity = Vector3.Lerp(
    //        currentVelocity,
    //        targetVelocity,
    //        10f * Time.fixedDeltaTime
    //    );

    //    rb.velocity = new Vector3(
    //        currentVelocity.x,
    //        rb.velocity.y,
    //        currentVelocity.z
    //    );

    //    // ===== ROTATION =====


    //    if (moveDir != Vector3.zero)
    //    {
    //        Quaternion targetRot = Quaternion.LookRotation(moveDir);

    //        playerState.transform.rotation = Quaternion.Slerp(
    //            playerState.transform.rotation,
    //            targetRot,
    //            PlayerController.Instance.rotationSpeed * Time.fixedDeltaTime
    //        );
    //    }
    //}
}
public class PlayerAttackState : State
{
    private PlayerStateMachine player;
    float attackCooldown = 1f;
    float timer;

    public PlayerAttackState(PlayerStateMachine player) : base(player)
    {
        this.player = player;
    }
    public override void Enter()
    {
        timer = attackCooldown;
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 2f))
        {
            HealthSystem enemy = hit.collider.GetComponent<HealthSystem>();
            if (enemy != null)
            {
                enemy.TakeDamage(20f);
            }
        }
        Debug.Log("Entering Attack State");
    }
    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            player.ChangeState(player.idleState);
        }
        // Add attack logic here
    }
    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}

