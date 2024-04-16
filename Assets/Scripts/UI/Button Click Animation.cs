using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class ButtonClickAnimation : MonoBehaviour 
{
    private Button _button;
    private Vector3 _upScale = new Vector3(1.1f, 1.1f, 1);

    private void Start() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickAnimation);
    }

    private void ClickAnimation() {
        transform.DOScale(_upScale, 0.1f);
        transform.DOScale(Vector3.one, 0.1f).SetDelay(0.1f);
    }
}