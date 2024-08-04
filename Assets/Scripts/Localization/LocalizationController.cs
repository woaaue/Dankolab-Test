using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationController : MonoBehaviour
{
    private int _currentLocalIndex = 0;

    public async void InitializeLocalizationAsync()
    {
        await LocalizationSettings.InitializationOperation.Task;
        SetLocale();
    }

    private void SetLocale()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        if (locales.Count == 0)
            return;

        _currentLocalIndex = (_currentLocalIndex + 1) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[_currentLocalIndex];
    }
}
