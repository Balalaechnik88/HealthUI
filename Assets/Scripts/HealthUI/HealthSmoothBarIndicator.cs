using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthSmoothBarIndicator : HealthIndicatorBase
{
    [SerializeField] private Slider _slider;

    [Header("Smoothing")]
    [SerializeField] private float _fillSpeed = 1.5f;

    private float _minValue = 0f;
    private float _maxValue = 1f;
    private float _target;
    private Coroutine _routine;

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
        _target = Normalize(current, max);
        _slider.value = _target;
    }

    protected override void HandleHealthChanged(int current, int max)
    {
        _target = Normalize(current, max);

        if (_routine != null)
            StopCoroutine(_routine);

        _routine = StartCoroutine(SmoothRoutine());
    }

    protected override void OnDisabled()
    {
        if (_routine != null)
            StopCoroutine(_routine);

        _routine = null;
    }

    private IEnumerator SmoothRoutine()
    {
        while (Mathf.Approximately(_slider.value, _target) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _target, _fillSpeed * Time.deltaTime);
            yield return null;
        }

        _slider.value = _target;
        _routine = null;
    }
}
