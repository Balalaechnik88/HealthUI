using UnityEngine;
using UnityEngine.UI;

public sealed class HealthSmoothBarIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    [Header("Smoothing")]
    [SerializeField] private float _fillSpeed = 1.5f;

    private float _minValue = 0f;
    private float _maxValue = 1f;
    private float _targetValue;

    private void Awake()
    {
        if (_health == null || _slider == null)
        {
            Debug.LogError($"[{nameof(HealthSmoothBarIndicator)}] Не назначены ссылки. Скрипт отключён.", this);
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
        SetImmediate();
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _fillSpeed * Time.deltaTime);
    }

    private void OnHealthChanged(int current, int max)
    {
        _targetValue = CalculateNormalized(current, max);
    }

    private void SetImmediate()
    {
        _targetValue = CalculateNormalized(_health.CurrentHealth, _health.MaxHealth);
        _slider.value = _targetValue;
    }

    private static float CalculateNormalized(int current, int max)
    {
        if (max <= 0)
            return 0f;

        return Mathf.Clamp01((float)current / max);
    }
}
