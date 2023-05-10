using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseUI : MonoBehaviour
{
    private Transform _pausePanel;
    private GameObject _pauseButton;
    private GameObject _closeButton;
    private float _prevTimeScale;

    private void Awake()
    {
        _pausePanel = transform.parent;
        _pauseButton = _pausePanel.Find("PauseButton")?.gameObject;
        _closeButton = _pausePanel.Find("CloseButton")?.gameObject;
        _prevTimeScale = Time.timeScale;
    }

    public void OnEnable()
    {
        _prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        _pauseButton.SetActive(false);
        _closeButton.SetActive(true);
        AudioManager.Instance.PauseAll();
    }

    public void OnDisable()
    {
        Time.timeScale = _prevTimeScale;
        _pauseButton.SetActive(true);
        _closeButton.SetActive(false);
        AudioManager.Instance.PlayAll();
    }

    // UI Pause
    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        EventManager.Instance.PlayerRestartEventTrigger();
    }

    public void Select()
    {
        SceneManager.LoadScene("UI_Select");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
