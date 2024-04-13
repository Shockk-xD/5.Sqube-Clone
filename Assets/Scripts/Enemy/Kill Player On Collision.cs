using UnityEngine;

public class KillPlayerOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player") && !PlayerEnemyInteraction.IsInvincible) {
            var playerEnemyInteraction = collision.gameObject.GetComponent<PlayerEnemyInteraction>();
            playerEnemyInteraction.KillPlayer();
        }
    }
}
