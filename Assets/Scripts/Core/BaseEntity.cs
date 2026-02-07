using UnityEngine;

namespace Game.Core {
    public abstract class BaseEntity : MonoBehaviour {
        [Header("Entity Settings")]
        public int health = 100;
        public float moveSpeed = 5f;

        // Hàm xử lý nhận sát thương dùng chung
        public virtual void TakeDamage(int amount) {
            health -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Health left: {health}");
            if (health <= 0) Die();
        }

        // Buộc các lớp con (Player, Enemy) phải tự viết hàm chết riêng
        public abstract void Die();
    }
}