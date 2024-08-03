using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dankolab-Test/Settings/UpgradesSettings", fileName = "UpgradesSettings", order = 1)]
public sealed class UpgradesSettings : ScriptableObject
{
    private const string PATH = "Assets/Resources/Settings/UpgradeSettings/";

    [SerializeField] private int _countUpgrades;
    [SerializeField] private float _coefficientPrice;

    [field: SerializeField] public List<UpgradeSettings> Upgrades { get; private set; }


    [Button("Generate Upgrade Settings")]
    private void CreateUpgradesSettings()
    {
        DeleteAllUpgradeSettings();
        GenerateUpgradeSettings();
    }

    private void DeleteAllUpgradeSettings()
    {
        if (!Directory.Exists(PATH))
        {
            Debug.LogWarning($"Directory {PATH} does not exist.");
            return;
        }

        var upgradesSettings = Directory.GetFiles(PATH, "*.asset", SearchOption.TopDirectoryOnly).ToList();

        upgradesSettings.ForEach(upgrade =>
        {
            AssetDatabase.DeleteAsset(upgrade);
        });

        AssetDatabase.Refresh();
    }

    private void GenerateUpgradeSettings()
    {
        Upgrades = new List<UpgradeSettings>();

        for (int i = 0; i < _countUpgrades; i++)
        {
            var upgradeSettings = CreateInstance<UpgradeSettings>();

            upgradeSettings.UpgradeInfo = new UpgradeInfo();
            upgradeSettings.UpgradeInfo.Level = i;
            upgradeSettings.UpgradeInfo.PayPerClick = i + 1;
            upgradeSettings.UpgradeInfo.UpgradePrice = Mathf.RoundToInt((i + 1) * 10 * _coefficientPrice);

            string assetPath = $"{PATH}UpgradeSettings_{i}.asset";

            AssetDatabase.CreateAsset(upgradeSettings, assetPath);
            Upgrades.Add(upgradeSettings);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
