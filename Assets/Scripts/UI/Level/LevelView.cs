using TMPro;
using Zenject;
using UnityEngine;

public sealed class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevel;
    
    [Inject] private UpgradeController _upgradeController;

    private void Start()
    {
        OnLevelChanged();
        _upgradeController.UpgradeSettingsChanged += OnLevelChanged;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _currentLevel.text = $"LV {_upgradeController.GetCurrentLevel()}";
    }
}
