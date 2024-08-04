using TMPro;
using Zenject;
using System.Text;
using UnityEngine;

public sealed class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;

    [Inject] private MoneyController _controller;
    private StringBuilder _countMoneyText;

    private void Start()
    {
        _countMoneyText = new StringBuilder();

        _controller.MoneyChanged += OnCountMoneyChanged;
    }

    private void OnDestroy()
    {
        _controller.MoneyChanged -= OnCountMoneyChanged;
    }

    private void OnCountMoneyChanged(int value)
    {
        _countMoneyText.Clear();
        _countMoneyText.Append(value);

        _countMoney.text = _countMoneyText.ToString();
    }
}
