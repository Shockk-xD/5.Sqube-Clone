using System.Collections;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    [SerializeField] private GameObject _portal;

    public Transform[] gemUIParents;
    public int GemCount { get; private set; } = 0;
    public static readonly int MAX_GEM_COUNT = 2;

    public static GemCollector instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void IncrementGemCount() {
        GemCount++;

        if (GemCount == MAX_GEM_COUNT) {
            StartCoroutine(ActivatePortal());
        }
    }

    private IEnumerator ActivatePortal() {
        _portal.SetActive(true);
        float activateDuration = 0.5f;

        for (float s = 0; s < activateDuration; s += Time.deltaTime) {
            _portal.transform.localScale = Vector3.Lerp(
                Vector3.zero,
                Vector3.one,
                s / activateDuration
                );

            yield return null;
        }
    }
}
