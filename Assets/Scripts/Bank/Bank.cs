public sealed class Bank
{
    public int CurrentValue { get; private set; }

    public Bank()
    {
        CurrentValue = 0;
    }

    public void IncreaseMoney(int value)
    {
        CurrentValue += value;
    }

    public bool TryDecreaseMoney(int value) => CurrentValue - value >= 0;
}
