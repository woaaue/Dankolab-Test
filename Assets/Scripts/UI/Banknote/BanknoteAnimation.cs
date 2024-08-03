using DG.Tweening;
using UnityEngine;

public sealed class BanknoteAnimation : MonoBehaviour
{
    private Sequence _sequence;

    public void CreateSequence(Vector2 directionX)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOLocalJump(directionX, 150f, 1, 1.5f).SetEase(Ease.OutQuad))
            .Join(transform.DORotate(new Vector3(0, 0, Random.Range(360, 720)), 1.5f, RotateMode.FastBeyond360))
            .Join(transform.DOLocalMoveY(-Screen.height - 150, 3f).SetEase(Ease.InQuad).SetDelay(1f));

    }

    public void OnEnable()
    {
        _sequence.Play();
    }
}
