using System.Collections;
using UnityEngine;

public class ObserverBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _destroyDelay;

    public Vector2 targetPosition;

    private GameObject _particle;
    private float _destroyTimeMultiplier = 1;

    private void Start() {
        _particle = GetComponentInChildren<ParticleSystem>().gameObject;
        StartCoroutine(DestroyAfterDelay());
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            _bulletSpeed * Time.deltaTime
            );
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            if (collision.transform.CompareTag("Player")) {
                var playerEntityInteraction = collision.gameObject.GetComponent<PlayerEnemyInteraction>();
                playerEntityInteraction.KillPlayer();
            }
        }

        _destroyTimeMultiplier = 5;
        StartCoroutine(DestroyImmediately());
    }

    private IEnumerator DestroyImmediately() {
        GetComponent<Collider2D>().enabled = false;
        float destroyAnimationTime = 1f;
        for (float s = 0; s < destroyAnimationTime; s += Time.deltaTime * _destroyTimeMultiplier) {
            _particle.transform.localScale = 
            Vector3.Lerp(Vector3.one, Vector3.zero, s / destroyAnimationTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterDelay() {
        yield return new WaitForSeconds(_destroyDelay);
        StartCoroutine(DestroyImmediately());
    }
}
