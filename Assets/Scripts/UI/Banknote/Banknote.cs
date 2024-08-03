using UnityEngine;

public sealed class Banknote : MonoBehaviour
{
    [SerializeField] private BanknoteAnimation _animation;

    public void Setup(Vector2 _directionX)
    {
        _animation.CreateSequence(_directionX);
    }
}
