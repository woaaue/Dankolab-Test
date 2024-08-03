using TMPro;
using UnityEngine;

public sealed class MoneyView : MonoBehaviour
{
    [SerializeField] private MoneyController _controller;
    [SerializeField] private TextMeshProUGUI _countMoney;

    private void Start()
    {
        _controller.MoneyChanged += OnCountMoneyChanged;
        _countMoney.text = _controller.GetMoney().ToString();
    }

    private void OnDestroy()
    {
        _controller.MoneyChanged -= OnCountMoneyChanged;
    }

    private void OnCountMoneyChanged(int value)
    {
        _countMoney.text = value.ToString();
    }
}
