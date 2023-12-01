using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Object that that responsible for all UI changing logic
/// </summary>
public sealed class UIController : Singleton<UIController>
{
    /// <summary>
    /// Player new Unity InputSystem exit and pause actions
    /// </summary>
    [SerializeField] private InputActionReference _exit, _pause;

    [SerializeField] private Animator _animator, _gameOverAnimator;

    [SerializeField] private GameObject _gameOverImage;

    public event Action PlayerHealthChange;

    public event Action<Vector2, int> TakeDamage;
    public void OnTakeDamage(Vector2 position, int damage) => TakeDamage?.Invoke(position, damage);
    public void OnPlayerHealthChange() => PlayerHealthChange?.Invoke();

    /// <summary>
    /// OnEnable invokes on object creation , adds methods references to InputActions
    /// </summary>
    private void OnEnable()
    {
        _exit.action.performed += PerformExit;
        _pause.action.performed += PerformPause;
    }

    /// <summary>
    /// OnDisable invokes on object destruction , removes methods references from InputActions
    /// </summary>
    private void OnDisable()
    {
        _exit.action.performed -= PerformExit;
        _pause.action.performed -= PerformPause;
    }

    /// <summary>
    /// OnClick method for menu level changing
    /// </summary>
    public void ChangeLevel(int sceneIndex)
    {
        LoadingScreen.Instance.gameObject.SetActive(true);
        LoadingScreen.Instance.ChangeLevel(sceneIndex);
    }

    /// <summary>
    /// OnClick method for menu Exit button
    /// </summary>
    public void OnGameExit() => GameController.Instance.Exit();

    /// <summary>
    /// OnClick method for Restart button
    /// </summary>
    public void Restart() => GameController.Instance.Restart();

    /// <summary>
    /// Method that subscribes/unsubscribes to exit action
    /// </summary>
    /// <param name="obj"></param>
    private void PerformExit(InputAction.CallbackContext obj)
    {
        if (GameController.Instance.IsTimeStop) DisablePause();

        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex != 0)
            LoadingScreen.Instance.ChangeLevel(--activeSceneIndex);
        else
            GameController.Instance.Exit();
    }

    /// <summary>
    /// Method that subscribes/unsubscribes to pause action
    /// </summary>
    /// <param name="obj"></param>
    private void PerformPause(InputAction.CallbackContext obj)
    {
        if (GameController.Instance.IsTimeStop) DisablePause();
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            _animator.Play("PauseIn");
            GameController.Instance.SetPause();
        }
    }

    /// <summary>
    /// Method that disable pause
    /// </summary>
    /// <param name="obj"></param>
    public void DisablePause()
    {
        _animator.Play("PauseOut");
        GameController.Instance.DisablePause();
    }

    /// <summary>
    /// Method that perform game over
    /// </summary>
    /// <param name="obj"></param>
    public void PerformGameOver()
    {
        _animator.Play("RestartButtonAppears");
        _gameOverImage.SetActive(true);
        _gameOverAnimator.Play("GameOver");
    }
}