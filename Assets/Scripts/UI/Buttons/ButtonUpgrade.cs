using TMPro;
using Zenject;
using UnityEngine;
using JetBrains.Annotations;

public sealed class ButtonUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUpgrade;
    [SerializeField] private TextMeshProUGUI _priceUpgrade;
    
    private MoneyController _moneyController;
    private UpgradeController _upgradeController;

    [Inject]
    public void Construct(MoneyController moneyController, UpgradeController upgradeController)
    {
        _moneyController = moneyController;
        _upgradeController = upgradeController;

        _moneyController.MoneyChanged += ButtonStateSwitcher;
    }

    [UsedImplicitly]
    public void Click()
    {
        _upgradeController.BuyUpgrade();
        ButtonStateSwitcher();
    }

    private void OnDestroy()
    {
        _moneyController.MoneyChanged -= ButtonStateSwitcher;
    }

    private void ButtonStateSwitcher(int currentMoney = 0)
    {
        if (_moneyController.GetMoney() >= _upgradeController.GetPriceUpgrade())
        {
            _priceUpgrade.text = $" {_upgradeController.GetPriceUpgrade()}";
            _buttonUpgrade.SetActive(true);
        }
        else
        {
            _buttonUpgrade.SetActive(false);
        }
    }
}
