using UnityEngine;
namespace Game.Core
{
    public abstract class BaseEntity : MonoBehaviour
    {
        [Header("Base Stats")]
        //[SerializeField] protected int health = 100;
        [SerializeField] protected bool isDead = false;
        protected HealthSystem healthSystem;

        protected virtual void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
        }
        public virtual void TakeDamage(int amount)
        {
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(amount);

            }
        }

        public virtual void Die()
        {
            isDead = true;
            // Add death logic here (e.g., play animation, disable entity)
            Debug.Log($"{gameObject.name} has died.");
        }
    }
}