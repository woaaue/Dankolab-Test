using System;
using Zenject;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class UpgradeController : MonoBehaviour
{
    [Inject] private MoneyController _moneyController;

    public event Action UpgradeSettingsChanged;

    private LevelSystem _levelSystem;
    private List<UpgradeSettings> _settings => SettingsProvider.Get<UpgradesSettings>().Upgrades;

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

    public int GetCurrentLevel() => _levelSystem.CurrentLevel;
    public int GetMoneyPerClick() => _settings.First(setting => setting.UpgradeInfo.Level == GetCurrentLevel()).UpgradeInfo.PayPerClick;
    public int GetPriceUpgrade() => _settings.First(setting => setting.UpgradeInfo.Level == GetCurrentLevel()).UpgradeInfo.UpgradePrice;

    public void BuyUpgrade()
    {
        if (_moneyController.TryRemoveMoney(GetPriceUpgrade()))
        {
            _levelSystem.IncreaseLevel();
            UpgradeSettingsChanged?.Invoke();
        }
    }
}
