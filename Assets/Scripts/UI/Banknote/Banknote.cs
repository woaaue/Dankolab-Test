using Zenject;
using UnityEngine;

public sealed class Banknote : MonoBehaviour
{
    [SerializeField] private BanknoteAnimation _animation;

    BanknoteController _banknoteController;

    [Inject]
    public void Construct(BanknoteController banknoteController)
    {
        _banknoteController = banknoteController;
    }

    public void Setup(Vector2 _directionX)
    {
        _animation.CreateSequence(_directionX);
    }

    private void OnEnable()
    {
        _animation.SequenceCompleted += OnSequenceCompleted;
    }

    private void OnDisable()
    {
        _animation.SequenceCompleted -= OnSequenceCompleted;
    }

    private void OnSequenceCompleted()
    {
        _banknoteController.Pool.Release(this);
    }
}
