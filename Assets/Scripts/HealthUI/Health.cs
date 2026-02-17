using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxHealth = 5;

    [Header("Runtime (read only)")]
    [SerializeField] private int _currentHealth;

    public event Action<int, int> HealthChanged;
    public event Action Died;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || _currentHealth <= 0)
            return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0)
            Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || _currentHealth <= 0)
            return;

        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
