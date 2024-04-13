using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed = 2.5f;

    private void Update() {
        transform.position = Vector3.Lerp(
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            new Vector3(_target.position.x, transform.position.y, transform.position.z),
            _moveSpeed * Time.deltaTime
            );
    }
}
