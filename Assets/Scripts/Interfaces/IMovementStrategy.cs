using UnityEngine;

namespace Game.Interfaces
{
    public interface IMovementStrategy
    {
        void Move(Transform transform, Vector3 direction, float speed);
        // Giữ lại Collider để sau này xử lý check Grounded hoặc va chạm khi nhảy
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
                // Bạn có thể dùng 'col' ở đây để check va chạm trước khi cho phép nhảy
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}