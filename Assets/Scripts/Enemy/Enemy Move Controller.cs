using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] protected Transform _enemy;
    [SerializeField] protected float _speed = 5;
    [SerializeField] protected float _pointDelay = 0.5f;
    [SerializeField] protected Transform _startPoint;
    [SerializeField] protected Transform _endPoint;
    [SerializeField] private float _positionClampRepeatDelay = 0.25f;

    protected int _direction = -1;
    private bool _isDelay = false;

    private void Start() {
        InvokeRepeating(nameof(ClampPosition), 0, _positionClampRepeatDelay);
    }

    protected virtual void Update() {
        if (_isDelay) return;

        _enemy.Translate(_speed * Time.deltaTime * _direction * Vector2.right);
        CheckForReachDestination();
    }

    protected void CheckForReachDestination() {
        if (_enemy.position.x >= _startPoint.position.x || _enemy.position.x <= _endPoint.position.x) {
            _direction *= -1;
            _isDelay = true;
            Invoke(nameof(StopPointDelay), _pointDelay);
        }
    }

    private void StopPointDelay() {
        _isDelay = false;
    }

    protected void ClampPosition() {
        _enemy.position = new Vector2(
            Mathf.Clamp(_enemy.position.x, _endPoint.position.x, _startPoint.position.x),
            _enemy.position.y
            );
    }
}
