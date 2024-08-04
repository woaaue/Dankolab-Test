using System;
using UnityEngine;

public sealed class MoneyController : MonoBehaviour
{
    public event Action<int> MoneyChanged;

    private Bank _bank;

    private void Start()
    {
        _bank = Storage.Load<Bank>();

        if (_bank == null)
            _bank = new Bank();

        MoneyChanged?.Invoke(GetMoney());
    }

    private void OnDisable()
    {
        Storage.Save(_bank);
    }

    public void AddMoney(int value)
    {
        _bank.IncreaseMoney(value);
        MoneyChanged?.Invoke(GetMoney());
    }

    public bool TryRemoveMoney(int value)
    {
        if (_bank.TryDecreaseMoney(value))
        {
            MoneyChanged?.Invoke(GetMoney());
            return true;
        }

        return false;
    }

    public int GetMoney() => _bank.CurrentValue;
}
