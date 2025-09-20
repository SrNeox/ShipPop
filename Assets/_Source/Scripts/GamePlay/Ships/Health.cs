using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action HealthOver;
    public event Action HealthChanged;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Start()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            HealthChanged?.Invoke();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (CurrentHealth <= 0)
        {
            HealthOver?.Invoke();
        }
    }

    public void RestoreHealth(float health)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += health;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        HealthChanged?.Invoke();
    }

    public void Init(float valueHealth, float maxHealth)
    {
        CurrentHealth = valueHealth;
        MaxHealth = maxHealth;
    }
}
