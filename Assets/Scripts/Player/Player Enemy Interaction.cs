using System.Collections;
using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{
    [Header("For Kill/Respawn")]
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer[] _renderers;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private GameObject _deathParticle;
    [SerializeField] private GameObject _respawnParticle;

    [Space()]
    [SerializeField] private Animator _canvasAnimator;

    public static bool isRespawning = false;
    public static bool IsInvincible => PlayerHideAbility.IsHiding || PlayerEnemyInteraction.isRespawning; 

    public void KillPlayer() {
        SoundManager.instance.PlaySound(SoundManager.SoundType.Death);
        _canvasAnimator.SetTrigger("Death");
        KillComponents();
        Instantiate(_deathParticle, transform);
    }

    private void KillComponents() {
        _controller.enabled = false;
        _rb.bodyType = RigidbodyType2D.Static;
        foreach (var renderer in _renderers) {
            renderer.enabled = false;
        }
        _collider.enabled = false;
        _trail.enabled = false;
    }

    public void Respawn() {
        StartCoroutine(RespawnComponents());
        isRespawning = true;
        Invoke(nameof(StopRespawning), 5f);
    }

    private IEnumerator RespawnComponents() {
        _canvasAnimator.SetTrigger("Respawn");
        yield return new WaitForSeconds(1.5f);

        foreach (var renderer in _renderers) {
            renderer.enabled = true;
        }

        _trail.enabled = true;
        for (float s = 0; s < 0.5f; s += Time.deltaTime) {
            _rb.gameObject.transform.localScale = Vector3.Lerp(
                Vector3.zero,
                Vector3.one,
                s / 0.5f
                );
            yield return null;
        }
        Instantiate(_respawnParticle, transform);

        _collider.enabled = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _controller.enabled = true;
    }

    private void StopRespawning() {
        isRespawning = false;
    }
}
