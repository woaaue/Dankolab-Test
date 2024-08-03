using TMPro;
using UnityEngine;
using JetBrains.Annotations;

public sealed class ButtonClicker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countAddMoney;
    [SerializeField] private MoneyController _moneyController;
    [SerializeField] private UpgradeController _upgradeController;

    private void Start()
    {
        OnMoneyPerClickChanged();
        _upgradeController.UpgradeSettingsChanged += OnMoneyPerClickChanged;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnMoneyPerClickChanged;
    }

    [UsedImplicitly]
    public void Click()
    {
        _moneyController.AddMoney(_upgradeController.GetMoneyPerClick());
    }

    private void OnMoneyPerClickChanged()
    {
        _countAddMoney.text = _upgradeController.GetMoneyPerClick().ToString();
    }
}
