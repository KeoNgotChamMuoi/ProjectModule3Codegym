using UnityEngine;

namespace Game.Interfaces {
    public interface IMovementStrategy {
        void Move(Transform transform, Vector3 direction, float speed);
<<<<<<< Updated upstream
        void Jump(Rigidbody rb, float jumpForce);
=======
        void Jump(Rigidbody rb, Collider col, float jumpForce);
    }

    public class WalkingMovement : IMovementStrategy
    {
        public void Move(Transform transform, Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        public void Jump(Rigidbody rb, Collider col, float jumpForce)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
>>>>>>> Stashed changes
    }
}