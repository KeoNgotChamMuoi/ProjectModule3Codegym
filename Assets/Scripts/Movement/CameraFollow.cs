using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Player
    public Transform cameraPivot;     // Pivot (child của player)
    public Transform cam;             // Main Camera

    [Header("Distance")]
    public float defaultDistance = 4f;
    public float minDistance = 1f;
    public float smoothSpeed = 10f;

    [Header("Collision")]
    public LayerMask collisionMask;   // Lớp để kiểm tra va chạm

    float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        if (target == null || cameraPivot == null || cam == null)
            return;

        HandlePosition();
        HandleCollision();
    }

    // ================= FOLLOW PLAYER =================
    void HandlePosition()
    {
        // DÍNH CHẶT player (không delay)
        cameraPivot.position = target.position;
    }

    // ================= CAMERA COLLISION =================
    void HandleCollision()
    {
        Vector3 dir = -cameraPivot.forward;

        RaycastHit hit;

        float targetDistance = defaultDistance;

        if (Physics.Raycast(
            cameraPivot.position,
            dir,
            out hit,
            defaultDistance,
            collisionMask))
        {
            targetDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }

        currentDistance = Mathf.Lerp(
            currentDistance,
            targetDistance,
            smoothSpeed * Time.deltaTime
        );

        cam.localPosition = new Vector3(0, 0, -currentDistance);
    }
}

