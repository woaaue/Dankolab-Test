using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor;
using NaughtyAttributes;
using JetBrains.Annotations;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dankolab-Test/Settings/UpgradesSettings", fileName = "UpgradesSettings", order = 1)]
public sealed class UpgradesSettings : ScriptableObject
{
    private const string PATH = "Assets/Resources/Settings/UpgradeSettings/";

    [field: SerializeField] public int CountUpgrades { get; private set; }
    [field: SerializeField] public int CountMoneyPerClick { get; private set; }
    [field: SerializeField] public float CoefficientPrice { get; private set; }

    [field: SerializeField] public List<UpgradeSettings> Upgrades { get; private set; }

#if UNITY_EDITOR
    [UsedImplicitly]
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
        var moneyPerClick = 1;
        Upgrades = new List<UpgradeSettings>();

        for (int i = 0; i <= CountUpgrades; i++)
        {
            var upgradeSettings = CreateInstance<UpgradeSettings>();

            upgradeSettings.UpgradeInfo = new UpgradeInfo();
            upgradeSettings.UpgradeInfo.Level = i;
            upgradeSettings.UpgradeInfo.PayPerClick = i == 0 ? moneyPerClick : moneyPerClick += CountMoneyPerClick;
            upgradeSettings.UpgradeInfo.UpgradePrice = Mathf.RoundToInt((i + 1) * 10 * CoefficientPrice);

            string assetPath = $"{PATH}UpgradeSettings_{i}.asset";

            AssetDatabase.CreateAsset(upgradeSettings, assetPath);
            Upgrades.Add(upgradeSettings);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

#endif
}
