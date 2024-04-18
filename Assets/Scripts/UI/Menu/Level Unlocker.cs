using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] _levelButtons;
    
    private void Start()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].interactable = false;
        }

        for (int i = 0; i < PlayerPrefs.GetInt("Unlocked Level", 1); i++)
        {
            int levelIndex = i;
            _levelButtons[i].interactable = true;
            _levelButtons[i].onClick.AddListener(() =>
            {
                MenuUIController.instance.LoadScene(levelIndex + 1);
            });
        }
    }
}
