using UnityEngine;
using UnityEngine.Pool;

public sealed class BanknotePool : MonoBehaviour
{
    [SerializeField] private Banknote _prefab;
    [SerializeField] private int _minPoolSize;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private RectTransform _pacifier;

    public ObjectPool<Banknote> Pool { get; private set; }

    public void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        Pool = new ObjectPool<Banknote>(() =>
        {
            var banknote = Instantiate(_prefab, _parent);
            banknote.Setup(GetDirection());

            return banknote;
        },
        banknote =>
        {
            banknote.gameObject.SetActive(true);
        },
        banknote =>
        {
            banknote.gameObject.SetActive(false);
        },
        banknote =>
        {
            Destroy(banknote);
        }, false, _minPoolSize, _maxPoolSize);
    }

    private Vector2 GetDirection()
    {
        Vector2 pacifierPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            _parent,
            RectTransformUtility.WorldToScreenPoint(null, _pacifier.position),
            null,
            out pacifierPosition
        );

        var directionX = new Vector2(
            Random.Range(-Screen.width, Screen.width),
            pacifierPosition.y);

        return directionX;
    }
}
