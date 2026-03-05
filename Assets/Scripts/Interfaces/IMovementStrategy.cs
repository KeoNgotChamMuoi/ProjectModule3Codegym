using UnityEngine;

namespace Game.Interfaces
{
    public interface IMovementStrategy
    {
        void Move(Transform transform, Vector3 direction, float speed);
        void Jump(Rigidbody rb, float jumpForce);
    }
    public class WalkingMovement : IMovementStrategy
    {
        public void Move(Transform transform, Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        public void Jump(Rigidbody rb, float jumpForce)
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

    }
}