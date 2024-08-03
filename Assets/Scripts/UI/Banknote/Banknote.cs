using Zenject;
using UnityEngine;

public sealed class Banknote : MonoBehaviour
{
    [SerializeField] private BanknoteAnimation _animation;

    [Inject] BanknoteController _pool;

    private void OnEnable()
    {
        _animation.SequenceCompleted += OnSequenceCompleted;
    }

    private void OnDisable()
    {
        _animation.SequenceCompleted -= OnSequenceCompleted;
    }

    public void Setup(Vector2 _directionX)
    {
        _animation.CreateSequence(_directionX);
    }

    private void OnSequenceCompleted()
    {
        _pool.Pool.Release(this);
    }
}
