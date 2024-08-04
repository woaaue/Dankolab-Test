using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationLoader : MonoBehaviour
{
    private async void Start()
    {
        await LocalizationSettings.InitializationOperation.Task;
    }
}
