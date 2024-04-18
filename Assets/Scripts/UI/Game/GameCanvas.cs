using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static RectTransform canvasRect;

    private void Awake() {
        canvasRect = GetComponent<RectTransform>();
    }

    public void PlayUISound(SoundManager.SoundType soundType) {
        SoundManager.instance.PlaySound(soundType);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
