using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Common;

public class UIManager : MonoSingleton<UIManager>
{
    private bool isVideo = true;

    private Transform _pauseUI;

    protected override void Init()
    {
        _pauseUI = transform.Find("PauseUI");
        if (_pauseUI)
        {
            _pauseUI.Find("PausePanel")?.gameObject.SetActive(false);
            _pauseUI.Find("PauseButton")?.gameObject.SetActive(true);
            _pauseUI.Find("CloseButton")?.gameObject.SetActive(false);
        }
    }

    // UI Pause
    public void PausePreform(InputAction.CallbackContext obj)
    {
        if (_pauseUI &&
            (bool)(_pauseUI.Find("PausePanel")?.gameObject.activeSelf))
        {
            _pauseUI.Find("PausePanel")?.gameObject.SetActive(false);
            _pauseUI.Find("PauseButton")?.gameObject.SetActive(true);
            _pauseUI.Find("CloseButton")?.gameObject.SetActive(false);
        }
        else
        {
            _pauseUI?.Find("PausePanel")?.gameObject.SetActive(true);
            _pauseUI?.Find("PauseButton")?.gameObject.SetActive(false);
            _pauseUI?.Find("CloseButton")?.gameObject.SetActive(true);
        }
         
    }

    // UI Main
    public void GameStart()
    {
        
        //加载到“UI_Select”场景
        SceneManager.LoadScene("UI_Select");
    }

    public void GameOver()
    {
        //关闭游戏
        Application.Quit();
    }

    // UI Select
    public void SelectBack()
    {
        //加载到“UI_Select”场景
        SceneManager.LoadScene("UI_Main");

    }

    public void SelectL1()
    {
        if (isVideo)
        {
            //加载到“UI_Video”场景
            SceneManager.LoadScene("Video_UI");
            isVideo = false;
        }
        else
        {
            //加载到“UI_Select”场景
            SceneManager.LoadScene("Level0_DESIGN");
        }
        //加载到“UI_Select”场景
    }

    public void SelectL3()
    {
        SceneManager.LoadScene("LEVEL3_PLAYGROUND 1");
    }

    // UI End
    public void EndBack()
    {
        //加载到“UI_Select”场景
        SceneManager.LoadScene("UI_Select");
    }

    public void RestartL1()
    {
        
        SceneManager.LoadScene("Level0_DESIGN");
    }

    public void RestartL3()
    {

        SceneManager.LoadScene("LEVEL3_PLAYGROUND 1");
    }
}
