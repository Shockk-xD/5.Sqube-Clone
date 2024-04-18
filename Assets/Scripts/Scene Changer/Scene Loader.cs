using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TransitionController _transitionController;
    
    public static SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        _transitionController.doTransition.Invoke();
        yield return new WaitForSeconds(2.5f);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        _transitionController.doTransition.Invoke();
    }
}
