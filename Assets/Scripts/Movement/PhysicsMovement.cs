using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Interfaces;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour, IMovementStrategy
{
    public float distanceToGround = 0.05f; //khoảng cách đến mặt đất
    public LayerMask layer; // các layer mà entity có thể nhảy khi đứng trên đó
    private Rigidbody rb;
    void Awake() 
    {  
        rb = GetComponent<Rigidbody>(); 
    }
    public void Jump(Rigidbody rb, Collider col, float jumpForce)
    {
        if (!isGround(col, distanceToGround, layer)) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Move(Transform transform, Vector3 direction, float speed)
    {
        Vector3 desiredVelocity = direction.normalized * speed - rb.velocity;
        rb.AddForce(desiredVelocity, ForceMode.Acceleration);
    }
    public static bool isGround(Collider collider, float distToGround, LayerMask layerMask)
    {
        Vector3 origin = collider.bounds.min;
        return Physics.Raycast(origin, Vector3.down, distToGround, layerMask, QueryTriggerInteraction.Ignore);
    }
}
