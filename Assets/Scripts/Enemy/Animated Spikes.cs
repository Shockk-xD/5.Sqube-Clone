using System.Collections;
using UnityEngine;

public class AnimatedSpikes : MonoBehaviour
{
    [SerializeField] private float _animationDuration = 0.25f;
    [SerializeField] private float _timeBetweenAnimations = 2f;
    
    private readonly float _yOffset = 0.25f;
    private float _startY;

    private float XPosition => transform.localPosition.x;

    private void Start() {
        _startY = transform.localPosition.y;
        StartCoroutine(Animation());
    }

    private IEnumerator Animation() {
        SoundManager.instance.PlaySound(SoundManager.SoundType.AnimatedSpikesClose, transform.position);
        yield return StartCoroutine(DownAnimation());
        yield return new WaitForSeconds(_timeBetweenAnimations);
        SoundManager.instance.PlaySound(SoundManager.SoundType.AnimatedSpikesOpen, transform.position);
        yield return StartCoroutine(UpAnimation());
        yield return new WaitForSeconds(_timeBetweenAnimations);
        StartCoroutine(Animation());
        
        IEnumerator DownAnimation() {
            for (float s = 0; s <= _animationDuration; s += Time.deltaTime) {
                var t = s / _animationDuration;

                transform.localPosition = new Vector2(
                    XPosition,
                    Mathf.Lerp(_startY, _startY - 1 - _yOffset, t)
                    );

                yield return null;
            }
        }

        IEnumerator UpAnimation() {
            for (float s = 0; s <= _animationDuration; s += Time.deltaTime) {
                var t = s / _animationDuration;

                transform.localPosition = new Vector2(
                    XPosition,
                    Mathf.Lerp(_startY - 1, _startY, t)
                    );

                yield return null;
            }
        }
    }
}
