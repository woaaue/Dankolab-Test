using UnityEngine;

public sealed class BanknoteController : MonoBehaviour
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private RectTransform _pacifier;
    [SerializeField] private BanknotePool _banknotePool;

    public void SpawnBanknote(Vector2 position)
    {
        var banknote = _banknotePool.Pool.Get();
        banknote.gameObject.transform.localPosition = position;
    }
}
