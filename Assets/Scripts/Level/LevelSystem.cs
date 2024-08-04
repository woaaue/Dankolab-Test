using System;

[Serializable]
public sealed class LevelSystem
{
    public int CurrentLevel;

    public void IncreaseLevel()
    {
        ++CurrentLevel;
    }
}
