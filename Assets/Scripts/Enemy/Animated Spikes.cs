using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatedSpikes : MonoBehaviour
{
    [SerializeField] private float _animationDuration = 0.25f;
    [SerializeField] private float _timeBetweenAnimations = 2f;
    
    private Transform[] _spikes;
    private float _startY;
    private Collider2D _collider2D;

    private float XPosition => transform.localPosition.x;

    private void Start() {
        _startY = transform.localPosition.y;
        _spikes = GetComponentsInChildren<Transform>();
        _collider2D = GetComponent<Collider2D>();
        StartCoroutine(Animation());
    }

    private IEnumerator Animation() {
        foreach (var spike in _spikes)
        {
            SoundManager.instance.PlaySound(SoundManager.SoundType.AnimatedSpikesClose, spike.position);
        }
        yield return StartCoroutine(DownAnimation());
        _collider2D.enabled = false;
        yield return new WaitForSeconds(_timeBetweenAnimations);
        foreach (var spike in _spikes)
        {
            SoundManager.instance.PlaySound(SoundManager.SoundType.AnimatedSpikesOpen, spike.position);
        }
        _collider2D.enabled = true;
        yield return StartCoroutine(UpAnimation());
        yield return new WaitForSeconds(_timeBetweenAnimations);
        StartCoroutine(Animation());
        
        IEnumerator DownAnimation() {
            for (float s = 0; s <= _animationDuration; s += Time.deltaTime) {
                var t = s / _animationDuration;

                transform.localPosition = new Vector2(
                    XPosition,
                    Mathf.Lerp(_startY, _startY - 1, t)
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
