using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Object that handle scene changing and LoadingScreen appearance
/// </summary>
public sealed class LoadingScreen : PersistentSingleton<LoadingScreen>
{
    /// <summary>
    /// Object that contains all nesassary stuff for LoadingScreen
    /// </summary>
    [SerializeField] private GameObject _loadingScreen;

    /// <summary>
    /// LoadingScreen CanvasGroup to interact with
    /// </summary>
    [SerializeField] private CanvasGroup _canvasGroup;

    /// <summary>
    /// LoadingScreen progressBar to track ChangeSceneAsync progress
    /// </summary>
    [SerializeField] private Image _progressBar;

    /// <summary>
    /// AsyncOperation for ChangeSceneAsync progress
    /// </summary>
    private AsyncOperation _loadingProgress;

    /// <summary>
    /// Update <see cref="_progressBar"/> from <see cref="_loadingProgress"/>
    /// </summary>
    private void Update()
    {
        if (_loadingProgress != null)
            _progressBar.fillAmount = Mathf.Clamp01(_loadingProgress.progress / 0.9f);
    }

    /// <summary>
    /// Entrypoint to start Level loading sequence
    /// </summary>
    public void ChangeLevel(int sceneIndex) => StartCoroutine(StartLoad(sceneIndex));

    /// <summary>
    /// Coroutine that track <see cref="_loadingProgress"/>
    /// </summary>
    private IEnumerator StartLoad(int sceneIndex)
    {
        _loadingScreen.SetActive(true);

        yield return StartCoroutine(FadeLoadingScreen(1, 1));


        _loadingProgress = GameController.Instance.ChangeSceneAsync(sceneIndex);

        while (!_loadingProgress.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));

        _loadingProgress = null;
        _progressBar.fillAmount = 0;
        _loadingScreen.SetActive(false);
    }

    /// <summary>
    /// Coroutine for <see cref="_canvasGroup"/> fade
    /// </summary>
    private IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = _canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = targetValue;
    }
}