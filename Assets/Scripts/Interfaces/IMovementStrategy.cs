using UnityEngine;

namespace Game.Interfaces
{
    public interface IMovementStrategy
    {
        void Move(Transform transform, Vector3 direction, float speed);
<<<<<<< HEAD
        void Jump(Rigidbody rb, Collider col, float jumpForce);
    }
=======
<<<<<<< Updated upstream
        void Jump(Rigidbody rb, float jumpForce);
=======
        void Jump(Rigidbody rb, Collider col, float jumpForce);
    }

>>>>>>> keongot
    public class WalkingMovement : IMovementStrategy
    {
        public void Move(Transform transform, Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
<<<<<<< HEAD
        public void Jump(Rigidbody rb, float jumpForce)
=======
        public void Jump(Rigidbody rb, Collider col, float jumpForce)
>>>>>>> keongot
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> Stashed changes
>>>>>>> keongot
    }
}