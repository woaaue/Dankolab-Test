using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

public sealed class ButtonClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _countAddMoney;
    [SerializeField] private RectTransform _canvasRectTransform;

    [Inject] private MoneyController _moneyController;
    [Inject] private UpgradeController _upgradeController;
    [Inject] private BanknoteController _banknoteController;

    private float _alphaTreshHold = 0.05f;

    private void Start()
    {
        _image.alphaHitTestMinimumThreshold = _alphaTreshHold;

        OnMoneyPerClickChanged();
        _upgradeController.UpgradeSettingsChanged += OnMoneyPerClickChanged;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnMoneyPerClickChanged;
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

        _moneyController.AddMoney(_upgradeController.GetMoneyPerClick());
        _banknoteController.SetSpawnPosition(localPoint);
    }

    private void OnMoneyPerClickChanged()
    {
        _countAddMoney.text = $"+ {_upgradeController.GetMoneyPerClick()}";
    }
}
