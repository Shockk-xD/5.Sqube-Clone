using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;

public class MenuUIController : MonoBehaviour
{
    [Header("======= Menu Panel =======")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Ease _menuPanelMoveEase;
    
    [Space(1), Header("======= Levels Panel =======")]
    [SerializeField] private RectTransform _levelsPanel;
    [SerializeField] private Ease _levelsPanelMoveEase;
    
    [Space(1), Header("======= Settings Panel =======")]
    [SerializeField] private RectTransform _settingsPanel;
    [SerializeField] private Ease _settingsPanelMoveEase;
    
    public static MenuUIController instance;

    private SceneLoader SceneLoader
    {
        get
        {
            if (_sceneLoader == null)
            {
                _sceneLoader = FindObjectOfType<SceneLoader>();
            }

            return _sceneLoader;
        }
    }

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public async void PlayButtonClick()
    {
        await HideMenuPanel();
        ShowLevelsPanel();
    }

    private async Task HideMenuPanel()
    {
        var direction = 1;
        var xOffset = 2000;
        var timeToMove = 0.4f;

        for (var i = 0; i < _menuPanel.transform.childCount; i++) {
            var childRect = _menuPanel.transform.GetChild(i).GetComponent<RectTransform>();
            SoundManager.instance.PlaySound(SoundManager.SoundType.UIMoveSound);
            await childRect.DOLocalMoveX(xOffset * direction, timeToMove).SetEase(_menuPanelMoveEase).AsyncWaitForCompletion();
            direction *= -1;
        }
    }

    private async void ShowMenuPanel()
    {
        var timeToMove = 0.4f;

        for (var i = 0; i < _menuPanel.transform.childCount; i++) {
            var childRect = _menuPanel.transform.GetChild(i).GetComponent<RectTransform>();
            SoundManager.instance.PlaySound(SoundManager.SoundType.UIMoveSound);
            await childRect.DOLocalMoveX(0, timeToMove).SetEase(_menuPanelMoveEase).AsyncWaitForCompletion();
        }
    }

    private void ShowLevelsPanel() {
        float targetY = 0;
        var duration = 1f;

        SoundManager.instance.PlaySound(SoundManager.SoundType.UIMoveSound);
        _levelsPanel.DOLocalMoveY(targetY, duration).SetEase(_levelsPanelMoveEase);
    }

    public async void SettingsButtonClick()
    {
        await HideMenuPanel();
        ShowSettingsPanel();
    }

    private void ShowSettingsPanel()
    {
        float targetY = 0;
        var duration = 1f;

        SoundManager.instance.PlaySound(SoundManager.SoundType.UIMoveSound);
        _settingsPanel.DOLocalMoveY(targetY, duration).SetEase(_settingsPanelMoveEase);
    }

    private async Task HideSettingsPanel()
    {
        float targetY = -1250;
        var duration = 1f;

        SoundManager.instance.PlaySound(SoundManager.SoundType.UIMoveSound);
        await _settingsPanel.DOLocalMoveY(targetY, duration).SetEase(_settingsPanelMoveEase).AsyncWaitForCompletion();
    }

    public async void CloseSettingsPanel()
    {
        await HideSettingsPanel();
        ShowMenuPanel();
    }

    public void LoadScene(int sceneIndex)
    {
        SceneLoader.LoadScene(sceneIndex);
    }
}
