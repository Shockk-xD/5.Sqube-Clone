using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [Header("General Settings")]
    [SerializeField] private int _maxPage;
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;
    [Space(), Header("Animation Settings")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private Ease _easeType;
    [Space(), Header("Level Bar")]
    [SerializeField] private Image[] _barImages;
    [SerializeField] private Color _barOpenColor;
    [SerializeField] private Color _barCloseColor;
    [Space(), Header("Array Buttons")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private int _currentPage;
    private Vector3 _targetPosition;
    private float _dragThreshould;

    private void Start() {
        _currentPage = 1;
        _targetPosition = _levelPagesRect.localPosition;
        _dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButtons();
    }

    public void Next() {
        if (_currentPage < _maxPage) {
            _currentPage++;
            _targetPosition += _pageStep;
            MovePage();
        }
    }

    public void Previous() {
        if (_currentPage > 1) {
            _currentPage--;
            _targetPosition -= _pageStep;
            MovePage();
        }
    }

    private void MovePage() {
        _levelPagesRect.DOLocalMove(_targetPosition, _tweenTime).SetEase(_easeType);
        UpdateBar();
        UpdateArrowButtons();
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > _dragThreshould) {
            if (eventData.position.x > eventData.pressPosition.x) {
                Previous();
            } else {
                Next();
            }
        } else {
            MovePage();
        }
    }

    private void UpdateBar() {
        foreach (var image in _barImages) {
            image.color = _barCloseColor;
        }
        _barImages[_currentPage - 1].color = _barOpenColor;
    }

    private void UpdateArrowButtons() {
        _nextButton.interactable = true;
        _previousButton.interactable = true;

        if (_currentPage == 1) {
            _previousButton.interactable = false;
        } else if (_currentPage == _maxPage) {
            _nextButton.interactable = false;
        }
    }
}
