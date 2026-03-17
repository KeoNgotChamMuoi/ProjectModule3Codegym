using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }
    public bool destroyOnDeath = true;
    public bool isDead = false;
    public event Action<float> OnDamage;
    public event Action OnDeath;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        OnDamage?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        OnDamage?.Invoke(0);
    }
    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnDamage?.Invoke(currentHealth);
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        OnDamage?.Invoke(currentHealth);
    }
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
