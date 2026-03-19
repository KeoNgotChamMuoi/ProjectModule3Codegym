using Game.Core;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public GameObject GameObject;
    public float currentHealth { get; private set; }
    public bool isDead;
    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        Debug.Log(gameObject.name + " - " + amount);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        Player3D player = GetComponent<Player3D>();
        if (player != null) { player.Die(); return; }
        EnemyBase enemy = GetComponent<EnemyBase>();
        if (enemy != null) { enemy.Die(); return; }
    }
    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;

    }
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
