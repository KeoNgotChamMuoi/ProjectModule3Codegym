using Game.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour, IMovementStrategy
{
    private Rigidbody rb;

    [Header("Flying Settings")]
    public float verticalSpeed = 5f; // speed move up/down

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Transform transform, Vector3 direction, float speed)
    {
        // Bỏ qua trọng lực
        rb.useGravity = false;

        // direction có thể chứa cả trục Y (bay lên/xuống)
        Vector3 desired = direction;


        Vector3 horizontal = new Vector3(desired.x, 0f, desired.z);
        Vector3 vertical = new Vector3(0f, desired.y, 0f);

        Vector3 vHoriz = horizontal.sqrMagnitude > 0.0001f ? horizontal.normalized * speed : Vector3.zero;
        Vector3 vVert = vertical.normalized * verticalSpeed;

        rb.velocity = vHoriz + vVert;
    }

    public void Jump(Rigidbody rb, Collider col, float jumpForce)
    {
        // Trong chế độ bay: Jump = bay lên (đặt trục Y = 1)
        Vector3 v = rb.velocity;
        v.y = verticalSpeed;
        rb.velocity = v;
    }

    public void SetFlying(bool isFlying)
    {
        rb.useGravity = !isFlying;
        if (!isFlying)
        {
            // Tắt fly: reset vận tốc theo trục y về 0 hoặc giữ vận tốc đang rơi (v.y<0)
            Vector3 v = rb.velocity;
            v.y = Mathf.Min(v.y, 0f);
            rb.velocity = v;
        }
        else
        {
            // Bật fly: dừng rơi
            Vector3 v = rb.velocity;
            v.y = 0f;
            rb.velocity = v;
        }
    }
}
