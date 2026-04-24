using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Targets")]
    public Transform target;      // Tham chiếu đến Player
    public Transform cameraPivot;   //  Điểm xoay của camera (thường là con trỏ rỗng gắn trên Player)
    public Transform cam;

    [Header("Settings")]
    public float defaultDistance = 4f;
    public float minDistance = 1f;
    public float smoothSpeed = 10f;
    public float mouseSensitivity = 100f;

    [Header("Collision")]
    public LayerMask collisionMask;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
        // Khóa con trỏ chuột trong màn hình game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!target || !cameraPivot || !cam) return;

        HandleRotation();
        HandlePosition();
        HandleCollision();
    }

    private void HandleRotation()
    {
        yRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -40f, 60f); // Góc nhìn TPS thực tế

        cameraPivot.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void HandlePosition()
    {
        // Pivot luôn bám sát vị trí Player
        cameraPivot.position = target.position;
    }

    private void HandleCollision()
    {
        Vector3 dir = -cameraPivot.forward;
        float targetDistance = defaultDistance;

        if (Physics.Raycast(cameraPivot.position, dir, out RaycastHit hit, defaultDistance, collisionMask))
        {
            targetDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, smoothSpeed * Time.deltaTime);
        cam.localPosition = new Vector3(0, 0, -currentDistance);
    }
}

