using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
