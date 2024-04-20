using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DontDestroyIfUnique), 0.1f);
    }

    private void DontDestroyIfUnique()
    {
        var soundManager = GetComponentInChildren<SoundManager>();

        if (soundManager)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
