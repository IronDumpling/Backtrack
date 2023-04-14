using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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
        //加载到“UI_Select”场景
        SceneManager.LoadScene("Level0_DESIGN");

    }
    public void GameOver()
    {
        //关闭游戏
        Application.Quit();
    }
}
