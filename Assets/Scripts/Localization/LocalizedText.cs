using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private bool _changeFont = false;

    private LocalizationManager _localizationManager;
    private Text _text;

    private void Awake()
    {
        _localizationManager =
            GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        _localizationManager.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = _localizationManager.GetLocalizedValue(_key);

        if (_changeFont)
        {
            _text.font = _localizationManager.CurrentLanguage == "en_US" ? _localizationManager.trajanProFont : _localizationManager.ubuntuFont;
        }
    }
    
    private void OnDestroy()
    {
        _localizationManager.OnLanguageChanged -= UpdateText;
    }
}
