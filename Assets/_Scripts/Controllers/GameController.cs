using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for game state that exist entire application lifetime <see cref="PersistentSingleton{T}"/>
/// </summary>
public sealed class GameController : PersistentSingleton<GameController>
{
    /// <summary>
    /// The boolean variable that displays whether time is stopped in the game
    /// </summary>
    private bool _isTimeStop = false;

    /// <inheritdoc cref="_isTimeStop"/>
    public bool IsTimeStop
    {
        get { return _isTimeStop; }
    }

    /// <summary>
    /// Load scene by index asynchronousli
    /// </summary>
    /// <param name="sceneIndex">scene build index</param>
    /// <returns><see cref="AsyncOperation"/> for Loading scene process </returns>
    public AsyncOperation ChangeSceneAsync(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);

    /// <summary>
    /// Exit application
    /// </summary>
    public void Exit() => Application.Quit();

    /// <summary>
    /// Set pause
    /// </summary>
    public void SetPause()
    {
        _isTimeStop = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Disable pause
    /// </summary>
    public void DisablePause()
    {
        _isTimeStop = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Method that restart this scene
    /// </summary>
    /// <param name="obj"></param>
    public void Restart()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadingScreen.Instance.ChangeLevel(activeSceneIndex);
    }
}