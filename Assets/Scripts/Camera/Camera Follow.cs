using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 5;
    [SerializeField] private BoxCollider2D _bounds;

    private Camera _camera;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private void Start() {
        _camera = Camera.main;

        _minX = _bounds.bounds.min.x + _camera.orthographicSize * Screen.width / Screen.height;
        _maxX = _bounds.bounds.max.x - _camera.orthographicSize * Screen.width / Screen.height;
        _minY = _bounds.bounds.min.y + _camera.orthographicSize;
        _maxY = _bounds.bounds.max.y - _camera.orthographicSize;
    }

    private void Update() {
        var targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);

        var clampedX = Mathf.Clamp(targetPosition.x, _minX, _maxX);
        var clampedY = Mathf.Clamp(targetPosition.y, _minY, _maxY);

        targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
    }
}
