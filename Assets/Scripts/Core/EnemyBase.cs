using UnityEngine;
using Game.Interfaces;

namespace Game.Core 
{
    public abstract class EnemyBase : BaseEntity 
    {
        [Header("Enemy Combat Settings")]
        protected int damageOutput = 10;        // Sát thương gây ra cho Player
        protected IMovementStrategy moveStrategy; // Chiến thuật di chuyển của quái
        protected Transform targetPlayer;       // Mục tiêu để đuổi theo

        protected virtual void OnCollisionEnter(Collision col) 
        {
            if (col.gameObject.CompareTag("Player")) 
            {
                var player = col.gameObject.GetComponent<BaseEntity>();
                if (player != null) 
                {
                    player.TakeDamage(damageOutput);
                }
            }
        }
        public abstract void UpdateAI();
    }
}