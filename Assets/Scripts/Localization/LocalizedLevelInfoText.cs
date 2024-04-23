using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalizedLevelInfoText : MonoBehaviour
{
    [SerializeField] private bool _useLineBreak = false;
    [SerializeField] private bool _levelAutoSet = true;
    [SerializeField] private bool _changeFont = false;
    private Text _text;
    private int _level;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            TextUpdate();
        }
    }
    
    private void Start()
    {
        _text = GetComponent<Text>();
        
        Invoke(nameof(StartWithDelay), 0.1f);
    }

    private void StartWithDelay()
    {
        LocalizationManager.instance.OnLanguageChanged += TextUpdate;

        if (_levelAutoSet)
        {
            Level = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void TextUpdate()
    {
        _text.text = LocalizationManager.instance.GetLocalizedValue("Level") + (_useLineBreak ? "\n" : " ") + _level;

        if (_changeFont)
        {
            _text.font = LocalizationManager.instance.CurrentLanguage == "en_US"
                ? LocalizationManager.instance.trajanProFont
                : LocalizationManager.instance.ubuntuFont;
        }
    }

    private void OnDestroy()
    {
        LocalizationManager.instance.OnLanguageChanged -= TextUpdate;
    }
}
