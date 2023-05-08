using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseUI : MonoBehaviour
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.PauseAll();
    }

    public void OnDisable()
    {
        Time.timeScale = 1f;
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
