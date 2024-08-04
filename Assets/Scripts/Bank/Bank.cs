using System;

[Serializable]
public sealed class Bank
{
    public int CurrentValue;

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
