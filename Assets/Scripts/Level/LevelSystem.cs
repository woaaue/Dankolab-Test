public sealed class LevelSystem
{
    public int CurrentLevel { get; private set; }

    public LevelSystem()
    {
        CurrentLevel = 0;
    }

    public void RaiseLevel()
    {
        CurrentLevel++;
    }
}
