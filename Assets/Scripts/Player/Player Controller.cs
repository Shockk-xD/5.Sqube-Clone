using UnityEngine;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _flightSpeedMultiplier = 0.6f;
    [SerializeField] private float _additionalGravityScale = 2f;

    private Rigidbody2D _rb;
    private bool _isGrounded = false;
    private float _defaultJumpForce;

    private void Start() {
        _defaultJumpForce = _jumpForce;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Jump();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        var speedMultiplier = 1f;

        if (!_isGrounded) {
            speedMultiplier = _flightSpeedMultiplier;
        }

        var horizontalSpeed = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(horizontalSpeed * _speed * speedMultiplier, _rb.velocity.y);

        Rotate(horizontalSpeed);
    }

    private void Jump() {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            _rb.velocity = Vector2.up * _jumpForce;

            if (_jumpForce == _defaultJumpForce) {
                SoundManager.instance.PlaySound(SoundManager.SoundType.Jump);
            } else {
                SoundManager.instance.PlaySound(SoundManager.SoundType.HighJump);
            }
        }

        if (!_isGrounded) {
            _rb.AddForce(Vector2.down * _additionalGravityScale);
        }
    }

    private void Rotate(float horizontalSpeed) {
        if (horizontalSpeed > 0) {
            transform.eulerAngles = Vector2.zero;
        } else if (horizontalSpeed < 0) {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    public void SetJumpForce(float force = 0, bool setDefault = false) {
        if (setDefault) {
            _jumpForce = _defaultJumpForce;
            return;
        }

        _jumpForce = force;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        var isGrounded = false;

        for (var i = 0; i < collision.contactCount; i++) {
            var angle = Vector2.Angle(collision.contacts[i].normal, Vector2.up);
            if (angle < 45) {
                isGrounded = true;
                break;
            }
        }

        _isGrounded = isGrounded;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        _isGrounded = false;
    }
}
