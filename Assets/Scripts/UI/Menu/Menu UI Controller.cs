using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MenuUIController : MonoBehaviour
{
    [Header("Menu Panel")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Ease _menuPanelMoveEase;
    [Space(1), Header("Levels Panel")]
    [SerializeField] private RectTransform _levelsPanel;
    [SerializeField] private Ease _levelsPanelMoveEase;

    public async void PlayButtonClick() {
        var direction = 1;
        var xOffset = 2000;
        var timeToMove = 0.4f;

        for (var i = 0; i < _menuPanel.transform.childCount; i++) {
            var childRect = _menuPanel.transform.GetChild(i).GetComponent<RectTransform>();
            await childRect.DOLocalMoveX(xOffset * direction, timeToMove).SetEase(_menuPanelMoveEase).AsyncWaitForCompletion();
            direction *= -1;
        }

        ShowLevelsPanel();
    }

    private void ShowLevelsPanel() {
        float targetY = 0;
        var duration = 1f;

        _levelsPanel.DOLocalMoveY(targetY, duration).SetEase(_levelsPanelMoveEase);
    }
}
