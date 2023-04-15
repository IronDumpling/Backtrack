using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isVideo = true;
    public void GameStart()
    {
        
        //加载到“UI_Select”场景
        SceneManager.LoadScene("UI_Select");
    }
    public void Select_Back()
    {
        //加载到“UI_Select”场景
        SceneManager.LoadScene("UI_Main");

    }
    public void Select_01()
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
    public void GameOver()
    {
        //关闭游戏
        Application.Quit();
    }
}
