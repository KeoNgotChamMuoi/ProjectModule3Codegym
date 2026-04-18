using Game.Core;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public GameObject healthSystemObject;    // Tham chiếu đến GameObject chứa HealthSystem
    public float currentHealth { get; private set; }
    public bool isDead;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        OnHealthChanged?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
            Die();
        Debug.Log(gameObject.name + " - " + amount);
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        OnDeath?.Invoke(); // Gọi sự kiện khi nhân vật chết

        if (TryGetComponent<BaseEntity>(out var entity))
        {
            entity.Die();
        }

    }
    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);

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
