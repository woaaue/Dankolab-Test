using System.IO;
using UnityEngine;

public static class Storage
{
    public static T Load<T>()
    {
        var type = typeof(T);
        string filePath = GetFilePath(type.Name);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found: " + filePath);
            return default;
        }
    }

    public static void Save<T>(T data)
    {
        var type = typeof(T);
        string filePath = GetFilePath(type.Name);
        EnsureDirectoryExists(filePath);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    private static void EnsureDirectoryExists(string fileName)
    {
        string directoryPath = Path.GetDirectoryName(fileName);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }

    private static string GetFilePath(string fileName) => Path.Combine(Application.persistentDataPath, fileName + ".json");
}