using TMPro;
using UnityEngine;
using JetBrains.Annotations;

public sealed class ButtonUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUpgrade;
    [SerializeField] private TextMeshProUGUI _priceUpgrade;
    [SerializeField] private MoneyController _moneyController;
    [SerializeField] private UpgradeController _upgradeController;

    private void Start()
    {
        _moneyController.MoneyChanged += ButtonStateSwitcher;
    }

    private void OnDestroy()
    {
        _moneyController.MoneyChanged -= ButtonStateSwitcher;
    }

    [UsedImplicitly]
    public void Click()
    {
        _upgradeController.BuyUpgrade();
    }

    private void ButtonStateSwitcher(int currentMoney = 0)
    {
        if (_moneyController.GetMoney() >= _upgradeController.GetPriceUpgrade())
        {
            _priceUpgrade.text = $"Upgrade: {_upgradeController.GetPriceUpgrade()}";
            _buttonUpgrade.SetActive(true);
        }
        else
        {
            _buttonUpgrade.SetActive(false);
        }
    }
}
