using System.Collections;
using UnityEngine;

public class StoneGemUI : MonoBehaviour
{
    [SerializeField] private float _animationDuration = 2f;
    [SerializeField] private Vector2 _destinyWidthAndHeight;
    private RectTransform _rectTransform;

    private void Start() {
        _rectTransform = GetComponent<RectTransform>();

        GemCollector gemCollector = GemCollector.instance;
        _rectTransform.SetParent(gemCollector.gemUIParents[gemCollector.GemCount], true);
        gemCollector.IncrementGemCount();
        StartCoroutine(CollectAnimation());
    }

    private IEnumerator CollectAnimation() {
        for (float t = 0; t < _animationDuration; t += Time.deltaTime) {
            _rectTransform.localPosition = Vector2.Lerp(
                _rectTransform.localPosition,
                Vector2.zero,
                t / _animationDuration
                );

            _rectTransform.sizeDelta = Vector2.Lerp(
                _rectTransform.sizeDelta,
                _destinyWidthAndHeight,
                t / _animationDuration
                );

            yield return null;
        }
    }
}
