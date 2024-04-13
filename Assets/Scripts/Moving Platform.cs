using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _delayTime = 0.5f;
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    private Transform _targetPoint;
    private bool _isDelay = false;

    private void Start() {
        _targetPoint = _pointA;
    }

    private void Update() {
        if (_isDelay) {
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            _targetPoint.position,
            _speed * Time.deltaTime
            );

        if (Vector2.Distance(transform.position, _pointA.position) < 0.05) {
            _targetPoint = _pointB;
            StartDelay();
        }

        if (Vector2.Distance(transform.position, _pointB.position) < 0.05) {
            _targetPoint = _pointA;
            StartDelay();
        }
    }

    private void StartDelay() {
        _isDelay = true;
        Invoke(nameof(StopDelay), _delayTime);
    }

    private void StopDelay() {
        _isDelay = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision?.gameObject.CompareTag("Player") == true) {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision?.gameObject.CompareTag("Player") == true) {
            collision.transform.SetParent(null);
        }
    }
}
