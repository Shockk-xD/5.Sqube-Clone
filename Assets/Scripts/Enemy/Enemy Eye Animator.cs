using UnityEngine;

public class EnemyEyeAnimator : MonoBehaviour
{
    [SerializeField] private float _angle = 0;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed = 3;

    private void Update() {
        Animate();
    }

    private void Animate() {
        _angle += Time.deltaTime * _speed;

        var radians = _angle * Mathf.Deg2Rad;

        var x = Mathf.Cos(radians) * _radius;
        var y = Mathf.Sin(radians) * _radius;

        transform.localPosition = new Vector2(x, y);
    }
}
