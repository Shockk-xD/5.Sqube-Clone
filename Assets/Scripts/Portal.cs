using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    [SerializeField] private Animator _canvasAnimator;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.SetActive(false);
            _canvasAnimator.SetTrigger("Win");
            DeactivatePortal();
        }
    }

    private void DeactivatePortal() {
        StartCoroutine(DeactivateAnimation());
    }

    private IEnumerator DeactivateAnimation() {
        float deactivateDuration = 0.5f;

        for (float s = 0; s < deactivateDuration; s += Time.deltaTime) {
            _portal.transform.localScale = Vector3.Lerp(
                Vector3.one,
                Vector3.zero,
                s / deactivateDuration
                );
            yield return null;
        }
        _portal.SetActive(false);
    }
}
