using UnityEngine;

public class CircleEnemyLineRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() {
        var raycastHit = Physics2D.Raycast(transform.position, Vector2.down);

        if (raycastHit.collider != null) {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, raycastHit.point);
        } else {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
