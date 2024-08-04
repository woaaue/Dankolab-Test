using System;
using DG.Tweening;
using UnityEngine;

public sealed class BanknoteAnimation : MonoBehaviour
{
    public event Action SequenceCompleted;

    private Sequence _sequence;

    public void CreateSequence(Vector2 directionX)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOLocalJump(directionX, 150f, 1, 1.5f).SetEase(Ease.OutQuad))
            .Join(transform.DORotate(new Vector3(0, 0, UnityEngine.Random.Range(360, 720)), 1.5f, RotateMode.FastBeyond360))
            .Join(transform.DOLocalMoveY(-Screen.height - 150, 2f).SetEase(Ease.InQuad).SetDelay(1f))
            .OnComplete(() =>
            {
                SequenceCompleted?.Invoke();
            });
    }

    private void OnEnable()
    {
        _sequence.Play();
    }
}
