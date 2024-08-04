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

    private float _alphaTreshHold = 0.05f;
    private MoneyController _moneyController;
    private UpgradeController _upgradeController;
    private BanknoteController _banknoteController;

    [Inject]
    public void Construct(MoneyController moneyController, UpgradeController upgradeController, BanknoteController banknoteController)
    {
        _moneyController = moneyController;
        _upgradeController = upgradeController;
        _banknoteController = banknoteController;

        _upgradeController.UpgradeSettingsChanged += OnMoneyPerClickChanged;
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

    private void Start()
    {
        _image.alphaHitTestMinimumThreshold = _alphaTreshHold;
    }

    private void OnDestroy()
    {
        _upgradeController.UpgradeSettingsChanged -= OnMoneyPerClickChanged;
    }

    private void OnMoneyPerClickChanged()
    {
        _countAddMoney.text = $"+ {_upgradeController.GetMoneyPerClick()}";
    }
}
