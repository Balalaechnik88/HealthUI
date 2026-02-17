using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private float _minValue = 0f;
    private float _maxValue = 1f;

    private void Awake()
    {
        if (_health == null || _slider == null)
        {
            Debug.LogError($"[{nameof(HealthBarIndicator)}] Не назначены ссылки. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        _slider.interactable = false;
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        Refresh();
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int current, int max)
    {
        _slider.value = CalculateNormalized(current, max);
    }

    private void Refresh()
    {
        _slider.value = CalculateNormalized(_health.CurrentHealth, _health.MaxHealth);
    }

    private static float CalculateNormalized(int current, int max)
    {
        if (max <= 0)
            return 0f;

        return Mathf.Clamp01((float)current / max);
    }
}
