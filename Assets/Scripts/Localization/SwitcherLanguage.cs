using Zenject;
using UnityEngine;
using JetBrains.Annotations;

public sealed class SwitcherLanguage : MonoBehaviour
{
    [Inject] private LocalizationController _localizationController;

    [UsedImplicitly]
    public void SwitchLocale() => _localizationController.InitializeLocalizationAsync();
}