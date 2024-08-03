using UnityEngine;

[CreateAssetMenu(menuName = "Dankolab-Test/Settings/UpgradeSettings", fileName = "UpgradeSettings", order = 1)]
public sealed class UpgradeSettings : ScriptableObject
{
    [field: SerializeField] public UpgradeInfo UpgradeInfo;
}
