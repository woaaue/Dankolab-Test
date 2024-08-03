using UnityEngine;
using UnityEngine.Pool;

public sealed class BanknoteController : MonoBehaviour
{
    [SerializeField] private Banknote _prefab;
    [SerializeField] private int _minPoolSize;
    [SerializeField] private int _maxPoolSize;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private RectTransform _pacifier;

    public ObjectPool<Banknote> Pool { get; private set; }

    private Vector2 _position;

    public void Start()
    {
        CreatePool();
    }

    public void SetSpawnPosition(Vector2 position)
    {
        _position = position;
        Pool.Get();
    }

    private void CreatePool()
    {
        Pool = new ObjectPool<Banknote>(() =>
        {
            return Instantiate(_prefab, _parent);
        },
        banknote =>
        {
            banknote.Setup(GetDirection());
            banknote.gameObject.transform.localPosition = _position;
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
            Random.Range(-_pacifier.rect.width / 2, _pacifier.rect.width / 2),
            pacifierPosition.y);

        return directionX;
    }
}
