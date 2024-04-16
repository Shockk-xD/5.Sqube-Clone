using DG.Tweening;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _upScale = new Vector3(1.25f, 1.25f, 1);

    private readonly float _duration = 5f;

    private void Start() {
        InvokeRepeating(nameof(Animation), 0, _duration * 2);
    }

    private void Animation() {
        transform.DOScale(_upScale, _duration);
        transform.DOScale(Vector3.one, _duration).SetDelay(_duration);
    }
}
