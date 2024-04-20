using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private ParticleSystem _coinPickupParticle;

    private readonly float _destroyDuration = 0.5f;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked Level", 1);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel < unlockedLevel)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Player")) {
                var golds = PlayerPrefs.GetInt("Golds", 0);
                PlayerPrefs.SetInt("Golds", golds + _value);
                UIController.instance.UpdateGoldsUI(golds);
                SoundManager.instance.PlaySound(SoundManager.SoundType.CoinCollect);
                StartCoroutine(Destroy());
            }
        }
    }

    private IEnumerator Destroy() {
        if (_coinPickupParticle) {
            _coinPickupParticle.Play();
        }

        GetComponent<Collider2D>().enabled = false;
        
        for (float s = 0; s < _destroyDuration; s += Time.deltaTime) {
            var t = s / _destroyDuration;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return null;
        }
    }
}
