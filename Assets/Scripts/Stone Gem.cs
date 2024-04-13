using UnityEngine;

public class StoneGem : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _stoneGemUIPrefab;
    [SerializeField] private RectTransform _canvas;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (GemCollector.instance.GemCount >= GemCollector.MAX_GEM_COUNT) return;

        var stoneGemUI = Instantiate(_stoneGemUIPrefab, _canvas);
        var screenPoint = Camera.main.WorldToViewportPoint(_parent.position);
        stoneGemUI.GetComponent<RectTransform>().localPosition = screenPoint;
        Destroy(_parent.gameObject);
    }
}
