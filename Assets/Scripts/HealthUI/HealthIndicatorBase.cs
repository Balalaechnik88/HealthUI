using UnityEngine;

public abstract class HealthIndicatorBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;

    protected Health Health => _health;

    private void Awake()
    {
        if (_health == null)
        {
            Debug.LogError($"[{GetType().Name}] Health не назначен. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        if (ValidateView() == false)
        {
            Debug.LogError($"[{GetType().Name}] UI-ссылки не назначены. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        OnAwake();
    }

    private void OnEnable()
    {
        if (enabled == false)
            return;

        _health.HealthChanged += OnHealthChangedInternal;

        ApplyImmediate(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthChanged -= OnHealthChangedInternal;

        OnDisabled();
    }

    private void OnHealthChangedInternal(int current, int max)
    {
        HandleHealthChanged(current, max);
    }

    protected static float Normalize(int current, int max)
    {
        if (max <= 0)
            return 0f;

        return Mathf.Clamp01((float)current / max);
    }

    protected abstract bool ValidateView();

    protected virtual void OnAwake() { }

    protected abstract void ApplyImmediate(int current, int max);

    protected abstract void HandleHealthChanged(int current, int max);

    protected virtual void OnDisabled() { }
}
