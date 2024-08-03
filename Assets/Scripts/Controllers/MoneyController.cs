using System;
using UnityEngine;

public sealed class MoneyController : MonoBehaviour
{
    public event Action<int> MoneyChanged;
    
    private Bank _bank = new Bank();

    public void AddMoney(int value)
    {
        _bank.IncreaseMoney(value);
        MoneyChanged?.Invoke(GetMoney());
    }

    public void RemoveMoney(int value)
    {
        if (_bank.TryDecreaseMoney(value))
            MoneyChanged?.Invoke(GetMoney());
    }

    public int GetMoney() => _bank.CurrentValue;
}
