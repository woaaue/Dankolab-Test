public sealed class Bank
{
    public int CurrentValue { get; private set; }

    public void IncreaseMoney(int value)
    {
        CurrentValue += value;
    }

    public bool TryDecreaseMoney(int value)
    {
        if (CurrentValue - value >= 0)
        {
            CurrentValue -= value;
            return true;
        }

        return false;
    }
}
