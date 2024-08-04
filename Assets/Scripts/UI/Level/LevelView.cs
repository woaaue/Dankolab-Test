using TMPro;
using Zenject;
using UnityEngine;

public sealed class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevel;
    
    private UpgradeController _upgradeController;

    [Inject]
    public void Construct(UpgradeController upgradeController)
    {
        _upgradeController = upgradeController;
        _upgradeController.UpgradeSettingsChanged += OnLevelChanged;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _currentLevel.text = $" {_upgradeController.GetCurrentLevel()}";
    }
}
