using UnityEngine;

public class StoneGem : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _stoneGemUIPrefab;
    
    private RectTransform _canvasRect;

    private void Start() {
        _canvasRect = GameCanvas.canvasRect;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (StoneCollector.instance.GemCount >= StoneCollector.MAX_GEM_COUNT) return;

        var stoneGemUI = Instantiate(_stoneGemUIPrefab, _canvasRect);
        var screenPoint = Camera.main.WorldToViewportPoint(_parent.position);
        stoneGemUI.GetComponent<RectTransform>().localPosition = screenPoint;
        Destroy(_parent.gameObject);
    }
}
