using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class InputManager : MonoBehaviour
{
    [Header("Physics & Ground Check")]
    public float gravity = -9.81f;
    public static InputManager Instance;
    [Header("Animator")]
    private Animator animator;
    [Header("Movement & Rotation Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Vector3 velocity;
    private CharacterController controller;

    // ===== GROUND CHECK =====
    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.2f;
    public bool isGrounded;

    // ===== INPUT DATA  =====
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public bool MouseLeft { get; private set; }
    public bool MouseLeftDown { get; private set; }
    public bool MouseRight { get; private set; }
    public bool MouseRightDown { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ReadKeyboard();
        ReadMouse();
        ApplyGravity();
        ApplyMovement();
    }

    void ReadKeyboard()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.A)) x -= 1f;
        if (Input.GetKey(KeyCode.D)) x += 1f;
        if (Input.GetKey(KeyCode.S)) y -= 1f;
        if (Input.GetKey(KeyCode.W)) y += 1f;

        MoveInput = new Vector2(x, y).normalized;

        JumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void ReadMouse()
    {
        MouseDelta = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        MouseLeft = Input.GetMouseButton(0);
        MouseRight = Input.GetMouseButton(1);
        MouseLeftDown = Input.GetMouseButtonDown(0);
        MouseRightDown = Input.GetMouseButtonDown(1);
    }

    void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        if (animator != null)
        {
        animator.SetBool("Grounded", isGrounded); 
        }
    }
    void ApplyMovement()
    {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0)
    {
        velocity.y = -2f; 
    }

    Vector3 move = transform.right * MoveInput.x + transform.forward * MoveInput.y;

    if (move.magnitude > 0.1f) 
    {

        Quaternion targetRotation = Quaternion.LookRotation(move);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    controller.Move(move * moveSpeed * Time.deltaTime);
    controller.Move(move * moveSpeed * Time.deltaTime);

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
    if (animator != null)
    {
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("ForwardSpeed", MoveInput.magnitude * moveSpeed);
        float speed = MoveInput.magnitude * moveSpeed; 
        animator.SetFloat("ForwardSpeed", speed);
        animator.SetBool("InputDetected", MoveInput.magnitude > 0);
    }
    }
}