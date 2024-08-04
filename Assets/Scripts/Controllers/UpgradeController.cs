using System;
using Zenject;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class UpgradeController : MonoBehaviour
{
    public event Action UpgradeSettingsChanged;

    private LevelSystem _levelSystem;
    private MoneyController _moneyController;
    private List<UpgradeSettings> _settings => SettingsProvider.Get<UpgradesSettings>().Upgrades;

    [Inject]
    public void Construct(MoneyController moneyController)
    {
        _moneyController = moneyController;
    }

    public void BuyUpgrade()
    {
        if (_moneyController.TryRemoveMoney(GetPriceUpgrade()))
        {
            _levelSystem.IncreaseLevel();
            UpgradeSettingsChanged?.Invoke();
        }
    }

    public int GetCurrentLevel() => _levelSystem.CurrentLevel;
    public int GetMoneyPerClick() => _settings.First(setting => setting.UpgradeInfo.Level == GetCurrentLevel()).UpgradeInfo.PayPerClick;
    public int GetPriceUpgrade() => _settings.First(setting => setting.UpgradeInfo.Level == GetCurrentLevel()).UpgradeInfo.UpgradePrice;

    private void Start()
    {
        _levelSystem = Storage.Load<LevelSystem>();

        if (_levelSystem == null)
            _levelSystem = new LevelSystem();

        UpgradeSettingsChanged?.Invoke();
    }

    private void OnDisable()
    {
        Storage.Save(_levelSystem);
    }
}
