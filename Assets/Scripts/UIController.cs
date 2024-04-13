using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("UI Texts to update gold count")]
    [SerializeField] private Text _inGameText;
    [SerializeField] private Text _winPanelText;
    [SerializeField] private Text _deathPanelText;

    [Space()]
    [SerializeField] private PlayerEnemyInteraction _playerEnemyInteraction;
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private Animator _totalGoldsTextAnimator;

    private readonly int _respawnCost = 30;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        UpdateGoldsUI();
    }

    public void UpdateGoldsUI(int oldValue = 0) {
        int golds = PlayerPrefs.GetInt("Golds", 0);

        StopAllCoroutines();
        StartCoroutine(UpdateIGGoldsTextUI(oldValue, golds));
        _winPanelText.text = golds.ToString();
        _deathPanelText.text = $"Total Golds: {golds}";
    }

    private IEnumerator UpdateIGGoldsTextUI(int oldValue, int newValue) {
        for (int i = oldValue; i <= newValue; i++) {
            _inGameText.text = i.ToString();
            yield return new WaitForSeconds((0.15f * 5) / (newValue - oldValue));
        }
    }

    public void Respawn() {
        int golds = PlayerPrefs.GetInt("Golds", 0);

        _canvasAnimator.SetTrigger("Respawn");
        _playerEnemyInteraction.Respawn();
        if (golds > _respawnCost) {
            //PlayerPrefs.SetInt("Golds", golds - 30);
            UpdateGoldsUI();
        } else {

        }
    }
}
