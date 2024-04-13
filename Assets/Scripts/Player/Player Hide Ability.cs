using System.Collections;
using UnityEngine;

public class PlayerHideAbility : MonoBehaviour
{
    [SerializeField] protected GameObject _playerEye;
    public static bool IsHiding = false;

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider != null && collision.gameObject.CompareTag("Ground")) {
            if (IsHiding) {
                if (collision.contactCount <= 2) {
                    IsHiding = false;
                    Unhide();
                }
            } else { 
                for (int i = 0; i < collision.contactCount; i++) {
                    if (IsGrounded(collision) && Approximately(Vector2.Angle(collision.contacts[i].normal, Vector2.up), 90, 0.1f)) {
                        IsHiding = true;
                        Hide();
                        break;
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        IsHiding = false;
        Unhide();
    }

    private bool IsGrounded(Collision2D collision) {
        for (int i = 0; i < collision.contactCount; i++) {
            if (Vector2.Angle(collision.contacts[i].normal, Vector2.up) == 0) {
                return true;
            }
        }

        return false;
    }

    private void Hide() {
        _playerEye.SetActive(false);
    }

    private void Unhide() {
        _playerEye.SetActive(true);
    }

    private bool Approximately(float a, float b, float tolerance) {
        return Mathf.Abs(a - b) < tolerance;
    }
}

public class PlayerEyeAnimation : PlayerHideAbility
{
    private readonly float _blinkFrequency = 10;
    private readonly float _blinkDuration = 0.3f;
    private float _defaultEyeYScale;
    
    private void Start() {
        _defaultEyeYScale = _playerEye.transform.localScale.y;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink() {
        yield return new WaitForSeconds(_blinkFrequency);
        
        for (float s = 0; s < _blinkDuration / 2; s += Time.deltaTime) {
            _playerEye.transform.localScale = new Vector3(
                _playerEye.transform.localScale.x,
                Mathf.Lerp(_defaultEyeYScale, 0, s / (_blinkDuration / 2)),
                1
                );
        }

        for (float s = 0; s < _blinkDuration / 2; s += Time.deltaTime) {
            _playerEye.transform.localScale = new Vector3(
                _playerEye.transform.localScale.x,
                Mathf.Lerp(0, _defaultEyeYScale, s / (_blinkDuration / 2)),
                1
                );
        }

        StartCoroutine(Blink());
    }
}