using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarIndicator : HealthIndicatorBase
{
    [SerializeField] private Slider _slider;

    private float _minValue = 0f;
    private float _maxValue = 1f;

    protected override bool ValidateView()
    {
        return _slider != null;
    }

    protected override void OnAwake()
    {
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        _slider.interactable = false;
    }

    protected override void ApplyImmediate(int current, int max)
    {
        _slider.value = Normalize(current, max);
    }

    protected override void HandleHealthChanged(int current, int max)
    {
        _slider.value = Normalize(current, max);
    }
}
