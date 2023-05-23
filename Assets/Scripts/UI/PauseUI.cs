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
    [SerializeField] private string _bgmName;

    private void Awake()
    {
        _pausePanel = transform.parent;
        _pauseButton = _pausePanel.Find("PauseButton")?.gameObject;
        _closeButton = _pausePanel.Find("CloseButton")?.gameObject;
        _prevTimeScale = Time.timeScale;
        _bgmName = AudioManager.Instance?.GetMusicIsPlaying();
    }

    public void OnEnable()
    {
        _prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        _pauseButton.SetActive(false);
        _closeButton.SetActive(true);
        AudioManager.Instance?.Pause(_bgmName);
    }

    public void OnDisable()
    {
        Time.timeScale = _prevTimeScale;
        _pauseButton.SetActive(true);
        _closeButton.SetActive(false);
        AudioManager.Instance?.Play(_bgmName);
    }

    public void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    // UI Pause
    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        URPManager.Instance.SetRendererAsset(0);
        EventManager.Instance.PlayerRestartEventTrigger();
    }

    public void Select()
    {
        AudioManager.Instance?.StopAll();
        URPManager.Instance.SetRendererAsset(0);
        SceneManager.LoadScene("UI_Select"); 
    }

    public void Quit()
    {
        URPManager.Instance.SetRendererAsset(0);
        Application.Quit();
    }
}
