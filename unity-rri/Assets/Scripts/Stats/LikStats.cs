using System;
using UnityEngine;

public class LikStats : MonoBehaviour
{
    public GeneralStat maxHealth;

    public GeneralStat damage;
    public GeneralStat armor;
    public float CurrentHealth { get; private set; }

    public void Awake()
    {
        CurrentHealth = maxHealth.GetValue();
    }

    public virtual void Start()
    {
    }

    public event Action OnHealthReachedZero;

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Subtract damage from health
        CurrentHealth -= damage;

        // If we hit 0. Die.
        if (CurrentHealth <= 0)
        {
            OnHealthReachedZero?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth.GetValue());
    }
}