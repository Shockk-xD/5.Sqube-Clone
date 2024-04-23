using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChangeStyle : MonoBehaviour
{
    [SerializeField] private LocalizationManager _localizationManager;
    [SerializeField] private Text[] _languageButtonTexts;

    private void Start()
    {
        ChangeStyle(_localizationManager.CurrentLanguage);
    }

    public void ChangeStyle(string language)
    {
        foreach (var languageButtonText in _languageButtonTexts)
        {
            languageButtonText.fontStyle = FontStyle.Normal;
        }

        _languageButtonTexts.First(l => l.name == language).fontStyle = FontStyle.Italic;
    }
}
