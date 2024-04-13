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

        float radians = _angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians) * _radius;
        float y = Mathf.Sin(radians) * _radius;

        transform.localPosition = new Vector2(x, y);
    }
}
