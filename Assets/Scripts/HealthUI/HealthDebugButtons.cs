using UnityEngine;

public sealed class HealthDebugButtons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;

    [Header("Amounts")]
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private int _healAmount = 10;

    private void Awake()
    {
        if (_health == null)
        {
            Debug.LogError($"[{nameof(HealthDebugButtons)}] Не назначен Health. Скрипт отключён.", this);
            enabled = false;
        }
    }

    public void DealDamage()
    {
        if (enabled == false)
            return;

        _health.TakeDamage(_damageAmount);
    }

    public void Heal()
    {
        if (enabled == false)
            return;

        _health.Heal(_healAmount);
    }
}
