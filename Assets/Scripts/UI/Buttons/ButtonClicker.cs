using TMPro;
using Zenject;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

public sealed class ButtonClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _countAddMoney;
    [SerializeField] private RectTransform _canvasRectTransform;
    
    [Inject] private MoneyController _moneyController;
    [Inject] private UpgradeController _upgradeController;
    [Inject] private BanknoteController _banknoteController;

    private void Start()
    {
        OnMoneyPerClickChanged();
        _upgradeController.UpgradeSettingsChanged += OnMoneyPerClickChanged;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnMoneyPerClickChanged;
    }

    [UsedImplicitly]
    public void Click()
    {
        _moneyController.AddMoney(_upgradeController.GetMoneyPerClick());
    }

    [UsedImplicitly]
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        _banknoteController.SpawnBanknote(localPoint);
    }

    private void OnMoneyPerClickChanged()
    {
        _countAddMoney.text = _upgradeController.GetMoneyPerClick().ToString();
    }
}
