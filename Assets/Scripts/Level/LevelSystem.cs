public sealed class LevelSystem
{
    public int CurrentLevel { get; private set; }

    public void IncreaseLevel()
    {
        ++CurrentLevel;
    }
}
