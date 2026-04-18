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
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    void HandleMouseLook()
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
    void HandleMovement()
    {
        Vector2 input = InputManager.Instance.MoveInput;

        // Hướng theo camera (TPS)
        Vector3 camForward = cameraPivot.forward;
        Vector3 camRight = cameraPivot.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // ===== MOVE =====
        Vector3 moveDir = camForward * input.y + camRight * input.x;

        // KHÔNG XOAY PLAYER Ở ĐÂY

        Vector3 velocity = rb.velocity;

        velocity.x = moveDir.x * speed;
        velocity.z = moveDir.z * speed;

        rb.velocity = velocity;
    }

    // ================= JUMP =================
    void HandleJump()
    {
        if (InputManager.Instance.JumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // ================= GROUND =================
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}