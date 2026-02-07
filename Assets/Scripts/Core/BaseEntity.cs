using UnityEngine;
namespace Game.Core 
{
    public abstract class BaseEntity : MonoBehaviour 
    {
        [Header("Base Stats")]
        protected int health = 100;           
        protected float moveSpeed = 5f;       
        protected bool isDead = false;        

        public virtual void TakeDamage(int amount) 
        {
            if (isDead) return;
            health -= amount;
            if (health <= 0) Die();
        }

        public abstract void Die(); 
    }
}