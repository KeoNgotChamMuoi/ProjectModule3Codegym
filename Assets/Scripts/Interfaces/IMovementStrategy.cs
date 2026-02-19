using UnityEngine;

namespace Game.Interfaces {
    public interface IMovementStrategy {
        void Move(Transform transform, Vector3 direction, float speed);
        void Jump(Rigidbody rb, Collider col, float jumpForce);
    }
}