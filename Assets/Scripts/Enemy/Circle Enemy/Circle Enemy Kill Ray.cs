using UnityEngine;

public class CircleEnemyKillRay : MonoBehaviour
{
    private void Update() {
        var raycastHit = Physics2D.Raycast(transform.position, Vector2.down);

        if (raycastHit.collider != null) {
            if (raycastHit.collider.CompareTag("Player") && !PlayerEnemyInteraction.IsInvincible) {
                var playerEnemyInteraction = raycastHit.collider.gameObject.GetComponent<PlayerEnemyInteraction>();
                playerEnemyInteraction.KillPlayer();
            }
        }
    }
}
