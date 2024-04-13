using UnityEngine;

public class JumpBoostPlatform : MonoBehaviour
{
    [SerializeField] private float _boostJumpForce = 20;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Player")) {
                var playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.SetJumpForce(force: _boostJumpForce);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Player")) {
                var playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.SetJumpForce(setDefault: true);
            }
        }
    }
}
