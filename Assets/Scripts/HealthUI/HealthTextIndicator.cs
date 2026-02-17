using TMPro;
using UnityEngine;

public sealed class HealthTextIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        if (_health == null || _text == null)
        {
            Debug.LogError($"[{nameof(HealthTextIndicator)}] Не назначены ссылки. Скрипт отключён.", this);
            enabled = false;
            return;
        }
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
        SetText(current, max);
    }

    private void Refresh()
    {
        SetText(_health.CurrentHealth, _health.MaxHealth);
    }

    private void SetText(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
