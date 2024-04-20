using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private SceneLoader _sceneLoader;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        _sceneLoader = FindObjectOfType<SceneLoader>();
        UpdateGoldsUI();
    }

    public void UpdateGoldsUI(int oldValue = 0) {
        var golds = PlayerPrefs.GetInt("Golds", 0);

        StopAllCoroutines();
        StartCoroutine(UpdateIGGoldsTextUI(oldValue, golds));
        _winPanelText.text = golds.ToString();
        _deathPanelText.text = $"Total Golds: {golds}";
    }

    private IEnumerator UpdateIGGoldsTextUI(int oldValue, int newValue) {
        for (var i = oldValue; i <= newValue; i++) {
            _inGameText.text = i.ToString();
            yield return new WaitForSeconds((0.15f * 5) / (newValue - oldValue));
        }
    }

    public void Respawn() {
        var golds = PlayerPrefs.GetInt("Golds", 0);

        _canvasAnimator.SetTrigger("Respawn");
        _playerEnemyInteraction.Respawn();
        if (golds > _respawnCost) {
            //PlayerPrefs.SetInt("Golds", golds - 30);
            UpdateGoldsUI();
        } else {

        }
    }

    public void RetryButtonClick()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_sceneLoader)
        {
            _sceneLoader.LoadScene(currentSceneIndex);
        } 
    }

    public void MenuReturnButtonClick()
    {
        Time.timeScale = 1;
        if (_sceneLoader)
        {
            _sceneLoader.LoadScene(0);
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex != sceneCount && _sceneLoader)
        {
            _sceneLoader.LoadScene(nextSceneIndex);
        }
        else
        {
            _sceneLoader.LoadScene(0);
        }
    }

    public void PauseButtonClick()
    {
        _canvasAnimator.SetTrigger("Pause");
    }

    public void UnpauseButtonClick()
    {
        Time.timeScale = 1;
        _canvasAnimator.SetTrigger("Unpause");
    }
}
