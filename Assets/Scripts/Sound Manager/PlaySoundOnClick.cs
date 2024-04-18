using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private SoundManager.SoundType _soundType;
    
    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button)
        {
            button.onClick.AddListener(() =>
            {
                SoundManager.instance.PlaySound(_soundType);
            });
        }
    }
}
