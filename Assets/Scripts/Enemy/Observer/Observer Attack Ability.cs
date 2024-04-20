using System;
using UnityEngine;

public class ObserverAttackAbility : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _attackDelay = 2f;

    private Transform _playerTransform;
    private bool _canAttack = true;
    public bool isAngry = false;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update() {
        if (isAngry && _canAttack && !IsPlayerBehindCover()) {
            var newBullet = Instantiate(_bullet);
            newBullet.transform.position = transform.position;
            newBullet.GetComponent<ObserverBullet>().targetPosition = _playerTransform.position;
            _canAttack = false;
            Invoke(nameof(AllowAttack), _attackDelay);
        } 
    }

    private void AllowAttack() {
        _canAttack = true;
    }

    private bool IsPlayerBehindCover() {
        var raycastHit = Physics2D.Raycast(transform.position, -transform.up);
        if (raycastHit.collider != null) {
            return !raycastHit.collider.CompareTag("Player");
        }
        return false;
    }
}
