using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 10f;

    [Header("Camera")]
    public Transform cameraPivot;
    public float mouseSensitivity = 100f;

    private Rigidbody rb;
    private bool isGrounded;

    float xRotation = 0f;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (InputManager.Instance == null) return;

        HandleMouseLook();
        HandleJump();
    }

    void FixedUpdate()
    {
        if (InputManager.Instance == null) return;

        HandleMovement();
    }

    // ================= CAMERA LOOK =================
    private void HandleMouseLook()
    {
        Vector2 mouse = InputManager.Instance.MouseDelta;

        float mouseX = mouse.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouse.y * mouseSensitivity * Time.deltaTime;

        // ===== XOAY PLAYER (NGANG) =====
        transform.Rotate(Vector3.up * mouseX);

        // ===== XOAY CAMERA (DỌC) =====
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 60f);

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    // ================= MOVEMENT TPS =================
    private void HandleMovement()
    {
        Vector2 input = InputManager.Instance.MoveInput;
        if (input.magnitude < 0.1f)
        {
            StopMovement();
            return;
        }

        // 1. Tính toán hướng di chuyển dựa trên Camera Pivot
        Vector3 camForward = cameraPivot.forward;
        Vector3 camRight = cameraPivot.right;

        camForward.y = 0; // Triệt tiêu Y để không đi xuyên đất
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * input.y + camRight * input.x).normalized;

        // 2. Di chuyển Rigidbody (Giữ nguyên vận tốc Y cho trọng lực)
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        // 3. Xoay nhân vật mượt mà về hướng di chuyển
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void StopMovement()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    // ================= JUMP =================
    private void HandleJump()
    {
        if (InputManager.Instance.JumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }



    // ================= GROUND =================
    private void OnCollisionStay(Collision col) => CheckGround(col, true);
    private void OnCollisionExit(Collision col) => CheckGround(col, false);

    private void CheckGround(Collision col, bool state)
    {
        if (col.gameObject.CompareTag("Ground")) isGrounded = state;
    }
}