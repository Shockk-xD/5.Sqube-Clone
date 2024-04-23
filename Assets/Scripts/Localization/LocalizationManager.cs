using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private string _currentLanguage;
    private string _dataAsJson;
    private Dictionary<string, string> _localizedText = new();
    
    public delegate void ChangleLanguageText();
    public event ChangleLanguageText OnLanguageChanged;

    public Font trajanProFont;
    public Font ubuntuFont;

    public static LocalizationManager instance;
    
    public string CurrentLanguage
    {
        get
        {
            if (string.IsNullOrEmpty(_currentLanguage))
            {
                _currentLanguage = PlayerPrefs.GetString("Language", "en_US");
            }

            return _currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            _currentLanguage = value;
            LoadLocalizedText(value);
        }
    }
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        LoadLocalizedText(CurrentLanguage);
    }

    public void ChangeLanguage(string langName)
    {
        CurrentLanguage = langName;
    }

    public void LoadLocalizedText(string langName)
    {
        var path = $"{Application.streamingAssetsPath}/Languages/{langName}.json";
        if (File.Exists(path))
        {
            _localizedText = new();
            
            _dataAsJson = File.ReadAllText(path);
            var loadedData = JsonUtility.FromJson<LocalizationData>(_dataAsJson);
            foreach (var item in loadedData.items)
            {
                _localizedText.Add(item.key, item.value);
            }

            OnLanguageChanged?.Invoke();
        }
        else
        {
            throw new Exception("Cannot find file!");
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (_localizedText.ContainsKey(key))
        {
            return _localizedText[key];
        }
        else
        {
            throw new KeyNotFoundException();
        }
    }
}
