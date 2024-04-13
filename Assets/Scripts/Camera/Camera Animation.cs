using System.Collections;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _offset;
    [SerializeField] private float _timeToOffset;

    private void Start() {
        StartCoroutine(CameraAnimationRoutine());
    }

    private IEnumerator CameraAnimationRoutine() {
        yield return StartCoroutine(RightAnimation());
        yield return StartCoroutine(LeftAnimation());
        StartCoroutine(CameraAnimationRoutine());

        IEnumerator RightAnimation() {
            Quaternion startRotation = _camera.transform.rotation;
            Quaternion destinyRotation = Quaternion.Euler(0, 0, _offset);
            for (float s = 0; s < _timeToOffset; s += Time.deltaTime) {
                float t = s / _timeToOffset;
                _camera.transform.rotation = Quaternion.Slerp(startRotation, destinyRotation, t);
                yield return null;
            }
        }

        IEnumerator LeftAnimation() {
            Quaternion startRotation = _camera.transform.rotation;
            Quaternion destinyRotation = Quaternion.Euler(0, 0, -_offset);
            for (float s = 0; s < _timeToOffset; s += Time.deltaTime) {
                float t = s / _timeToOffset;
                _camera.transform.rotation = Quaternion.Slerp(startRotation, destinyRotation, t);
                yield return null;
            }
        }
    }
}
