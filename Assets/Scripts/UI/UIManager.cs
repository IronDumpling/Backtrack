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

    private GameObject _pauseUI;

    protected override void Init()
    {
        _pauseUI = GameObject.Find("PauseUI");
        _pauseUI?.SetActive(false);
    }

    // UI Pause
    public void PausePreform(InputAction.CallbackContext obj)
    {
        if(_pauseUI && _pauseUI.activeSelf) _pauseUI?.SetActive(false);
        else _pauseUI?.SetActive(true);
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
