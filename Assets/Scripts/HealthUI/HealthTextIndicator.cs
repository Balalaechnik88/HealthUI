using TMPro;
using UnityEngine;

public sealed class HealthTextIndicator : HealthIndicatorBase
{
    [SerializeField] private TMP_Text _text;

    protected override bool ValidateView()
    {
        return _text != null;
    }

    protected override void ApplyImmediate(int current, int max)
    {
        SetText(current, max);
    }

    protected override void HandleHealthChanged(int current, int max)
    {
        SetText(current, max);
    }

    private void SetText(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
