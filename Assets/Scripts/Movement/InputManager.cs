using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    // ===== MOVEMENT =====
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }

    // ===== MOUSE =====
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
    }

    void Update()
    {
        ReadKeyboard();
        ReadMouse();

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

        // CHỈ TRUE 1 FRAME
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void ReadMouse()
    {
        MouseDelta = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        //Hold
        MouseLeft = Input.GetMouseButton(0);
        MouseRight = Input.GetMouseButton(1);

        //Click 1 frame
        MouseLeftDown = Input.GetMouseButtonDown(0);
        MouseRightDown = Input.GetMouseButtonDown(1);

    }
}
