using System;
using UnityEngine;

public class ObserverMoveController : EnemyMoveController
{
    [SerializeField] private Transform _player;

    public bool shouldFollowPlayer = false;

    override protected void Update() {
        if (shouldFollowPlayer) {
            float direction = GetDirection(_player.position.x - _enemy.position.x);
            _enemy.transform.Translate(_speed * Time.deltaTime * direction * Vector2.right);
            ClampPosition();
        } else {
            base.Update();
        }
    }

    private int GetDirection(float number) {
        if (Mathf.Approximately(number, 0)) {
            return 0;
        } else {
            return number > 0 ? 1 : -1;
        }
    }

}
