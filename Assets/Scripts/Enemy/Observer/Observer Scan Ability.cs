using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObserverScanAbility : MonoBehaviour
{
    [SerializeField] private ObserverMoveController _observerMoveController;
    [SerializeField] private ObserverAttackAbility _attackAbility;
    [SerializeField] private float _maxRotZ = 45;
    [SerializeField] private float _animationTime = 5f;
    [SerializeField] private float _lookSpeed = 5;

    private Transform _playerTransform = null;
    private bool _isAngryToPlayer = false;

    private void Start() {
        StartCoroutine(Scan());
    }

    private void Update() {
        if (_isAngryToPlayer) {
            LookAtPlayer();
        }
    }

    private IEnumerator Scan() {
        yield return StartCoroutine(ToRight());
        yield return StartCoroutine(ToLeft());
        StartCoroutine(Scan());

        IEnumerator ToRight() {
            var identity = transform.localRotation;
            var destiny = Quaternion.Euler(0, 0, _maxRotZ);
            for (float s = 0; s < _animationTime; s += Time.deltaTime) {
                transform.localRotation = Quaternion.Slerp(
                    identity,
                    destiny,
                    s / _animationTime
                    );

                yield return null;
            }
        }

        IEnumerator ToLeft() {
            var identity = transform.localRotation;
            var destiny = Quaternion.Euler(0, 0, -_maxRotZ);
            for (float s = 0; s < _animationTime; s += Time.deltaTime) {
                transform.localRotation = Quaternion.Slerp(
                    identity,
                    destiny,
                    s / _animationTime
                    );
                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !PlayerEnemyInteraction.IsInvincible) {
            if (_playerTransform == null) {
                _playerTransform = collision.transform;
            }
            _isAngryToPlayer = true;
            _attackAbility.isAngry = true;
            StopAllCoroutines();
            _observerMoveController.shouldFollowPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _isAngryToPlayer = false;
            _attackAbility.isAngry = false;
            StartCoroutine(Scan());
            _observerMoveController.shouldFollowPlayer = false;
        }
    }

    private void LookAtPlayer() {
        if (_playerTransform) {
            var angle = Mathf.Atan2(_playerTransform.position.y - transform.position.y, _playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 0f, angle + 90), _lookSpeed * Time.deltaTime);
        }
    }
}
