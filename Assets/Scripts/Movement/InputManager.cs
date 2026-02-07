using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    // Movement
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }

    // Mouse
    public Vector2 MouseDelta { get; private set; }
    public bool MouseLeft { get; private set; }
    public bool MouseRight { get; private set; }

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
    }
}
