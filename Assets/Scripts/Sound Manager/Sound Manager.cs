using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private Sound[] _sounds;

    public static SoundManager instance;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public enum SoundType {
        Jump,
        HighJump,
        Death,
        AnimatedSpikesOpen,
        AnimatedSpikesClose,
        StoneCollect,
        OpeningSound,
        Ding,
        CoinCollect,
        ButtonClick,
        UIMoveSound
    }

    public void PlaySound(SoundType soundType, Vector3? point = null) {
        var randSound = GetRandomSoundClip(soundType);
        
        if (randSound) {
            if (point != null) {
                AudioSource.PlayClipAtPoint(randSound, point.Value);
            } else {
                _sfxSource.PlayOneShot(randSound);
            }
        }
    }

    public AudioClip GetRandomSoundClip(SoundType soundType) {
        var sound = _sounds.First(s => s.soundType == soundType);
        return sound.clips[Random.Range(0, sound.clips.Length)];
    }
}
