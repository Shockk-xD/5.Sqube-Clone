using UnityEngine;

public class MineEnemyMoveController : EnemyMoveController 
{
    [SerializeField] private float _followSpeed = 1.5f;

    private Transform _player = null;
    private bool _isAngry = false;

    override protected void Update() {
        if (!_isAngry) {
            _enemy.Translate(_direction * _speed * Time.deltaTime * Vector2.right);
            CheckForReachDestination();
        } else {
            if (_player != null) {
                _enemy.transform.position = new Vector2(
                    Mathf.MoveTowards(
                            _enemy.transform.position.x,
                            _player.transform.position.x,
                            _followSpeed * Time.deltaTime
                            ),
                    _enemy.transform.position.y
                    );
                ClampPosition();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _isAngry = true;
            _player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _isAngry = false;
            _player = null;
        }
    }
}
